using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using SheetYar.Application.Authentication;
using SheetYar.Application.Errors;
using SheetYar.Domain.Authentication;
using SheetYar.Domain.Users;
using SheetYar.Infrastructure.Persistence;

namespace SheetYar.Infrastructure.Authentication;

internal sealed class AuthenticationService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    SheetYarDbContext dbContext,
    IAccessTokenIssuer accessTokenIssuer,
    IRefreshTokenFactory refreshTokenFactory,
    LoginTimingProtector loginTimingProtector,
    IOptions<JwtOptions> jwtOptions,
    TimeProvider timeProvider) : IAuthenticationService
{
    public async Task<AuthSession> RegisterAsync(
        string email,
        string password,
        string displayName,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = email.Trim();
        return await ExecuteSerializableAsync(async () =>
        {
            if (await userManager.FindByEmailAsync(normalizedEmail) is not null)
            {
                throw new ConflictException("An account with this email address already exists.");
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = normalizedEmail,
                UserName = normalizedEmail,
                DisplayName = displayName.Trim(),
                IsActive = true,
                LockoutEnabled = true,
            };
            var createResult = await userManager.CreateAsync(user, password);
            ThrowIfIdentityFailed(createResult);

            return await CreateNewSessionAsync(user, clientIpAddress, cancellationToken);
        }, cancellationToken);
    }

    public async Task<AuthSession> LoginAsync(
        string email,
        string password,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await userManager.FindByEmailAsync(email.Trim());
        if (user is null)
        {
            loginTimingProtector.VerifyUnknownUserPassword(password);
            throw new AuthenticationFailedException();
        }

        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            password,
            lockoutOnFailure: true);
        if (!result.Succeeded || !user.IsActive)
        {
            throw new AuthenticationFailedException();
        }

        return await ExecuteSerializableAsync(
            () => CreateNewSessionAsync(user, clientIpAddress, cancellationToken),
            cancellationToken);
    }

    public async Task<AuthSession> RefreshAsync(
        string refreshToken,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        var tokenHash = refreshTokenFactory.Hash(refreshToken);
        Guid? compromisedFamilyId = null;

        try
        {
            var session = await ExecuteSerializableAsync<AuthSession?>(async () =>
            {
                var currentToken = await dbContext.RefreshTokens
                    .Include(token => token.AppUser)
                    .SingleOrDefaultAsync(
                        token => token.TokenHash.SequenceEqual(tokenHash),
                        cancellationToken);
                if (currentToken is null || !currentToken.AppUser.IsActive)
                {
                    throw new AuthenticationFailedException();
                }

                var now = timeProvider.GetUtcNow().ToUniversalTime();
                if (currentToken.RevokedAtUtc is not null || currentToken.ReplacedByTokenId is not null)
                {
                    await RevokeFamilyAsync(
                        currentToken.TokenFamilyId,
                        currentToken.AppUser,
                        "Refresh token reuse detected",
                        clientIpAddress,
                        now,
                        cancellationToken);
                    return null;
                }

                if (currentToken.ExpiresAtUtc <= now)
                {
                    RevokeToken(currentToken, "Refresh token expired", clientIpAddress, now);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return null;
                }

                compromisedFamilyId = currentToken.TokenFamilyId;
                var replacement = refreshTokenFactory.Create(
                    currentToken.AppUserId,
                    currentToken.TokenFamilyId,
                    currentToken.ExpiresAtUtc,
                    clientIpAddress);
                RevokeToken(currentToken, "Refresh token rotated", clientIpAddress, now);
                currentToken.ReplacedByTokenId = replacement.Entity.Id;
                dbContext.RefreshTokens.Add(replacement.Entity);
                await dbContext.SaveChangesAsync(cancellationToken);

                return CreateSession(currentToken.AppUser, replacement);
            }, cancellationToken);

            return session ?? throw new AuthenticationFailedException();
        }
        catch (Exception exception) when (
            compromisedFamilyId.HasValue && IsRefreshRace(exception))
        {
            dbContext.ChangeTracker.Clear();
            await RevokeCompromisedFamilyAfterRaceAsync(
                compromisedFamilyId.Value,
                clientIpAddress,
                cancellationToken);
            throw new AuthenticationFailedException();
        }
    }

    public async Task LogoutAsync(
        Guid userId,
        string refreshToken,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        var tokenHash = refreshTokenFactory.Hash(refreshToken);
        var token = await dbContext.RefreshTokens.SingleOrDefaultAsync(
            candidate => candidate.AppUserId == userId && candidate.TokenHash.SequenceEqual(tokenHash),
            cancellationToken);
        if (token is null || token.RevokedAtUtc is not null)
        {
            return;
        }

        RevokeToken(
            token,
            "User logged out",
            clientIpAddress,
            timeProvider.GetUtcNow().ToUniversalTime());
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RevokeAllAsync(
        Guid userId,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        await ExecuteSerializableAsync(async () =>
        {
            var user = await userManager.FindByIdAsync(userId.ToString("D"));
            if (user is null || !user.IsActive)
            {
                throw new AuthenticationFailedException();
            }

            var now = timeProvider.GetUtcNow().ToUniversalTime();
            var activeTokens = await dbContext.RefreshTokens
                .Where(token => token.AppUserId == userId && token.RevokedAtUtc == null)
                .ToListAsync(cancellationToken);
            foreach (var token in activeTokens)
            {
                RevokeToken(token, "All sessions revoked by user", clientIpAddress, now);
            }

            var stampResult = await userManager.UpdateSecurityStampAsync(user);
            ThrowIfIdentityFailed(stampResult);
            await dbContext.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task<AuthenticatedUser> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.AppUsers
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate => candidate.Id == userId, cancellationToken);
        if (user is null || !user.IsActive)
        {
            throw new AuthenticationFailedException();
        }

        return ToAuthenticatedUser(user);
    }

    private async Task<AuthSession> CreateNewSessionAsync(
        AppUser user,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        var expiresAtUtc = timeProvider.GetUtcNow()
            .ToUniversalTime()
            .AddDays(jwtOptions.Value.RefreshTokenDays);
        var generatedToken = refreshTokenFactory.Create(
            user.Id,
            Guid.NewGuid(),
            expiresAtUtc,
            clientIpAddress);
        dbContext.RefreshTokens.Add(generatedToken.Entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return CreateSession(user, generatedToken);
    }

    private AuthSession CreateSession(AppUser user, GeneratedRefreshToken generatedToken)
    {
        var accessToken = accessTokenIssuer.Issue(user);
        return new AuthSession(
            "Bearer",
            accessToken.Value,
            accessToken.ExpiresAtUtc,
            generatedToken.Value,
            generatedToken.Entity.ExpiresAtUtc,
            ToAuthenticatedUser(user));
    }

    private async Task RevokeFamilyAsync(
        Guid familyId,
        AppUser user,
        string reason,
        string? clientIpAddress,
        DateTimeOffset revokedAtUtc,
        CancellationToken cancellationToken)
    {
        var activeTokens = await dbContext.RefreshTokens
            .Where(token => token.TokenFamilyId == familyId && token.RevokedAtUtc == null)
            .ToListAsync(cancellationToken);
        if (activeTokens.Count == 0)
        {
            return;
        }

        foreach (var token in activeTokens)
        {
            RevokeToken(token, reason, clientIpAddress, revokedAtUtc);
        }

        var stampResult = await userManager.UpdateSecurityStampAsync(user);
        ThrowIfIdentityFailed(stampResult);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task RevokeCompromisedFamilyAfterRaceAsync(
        Guid familyId,
        string? clientIpAddress,
        CancellationToken cancellationToken)
    {
        await ExecuteSerializableAsync(async () =>
        {
            var firstToken = await dbContext.RefreshTokens
                .Include(token => token.AppUser)
                .FirstOrDefaultAsync(token => token.TokenFamilyId == familyId, cancellationToken);
            if (firstToken is not null)
            {
                await RevokeFamilyAsync(
                    familyId,
                    firstToken.AppUser,
                    "Concurrent refresh token reuse detected",
                    clientIpAddress,
                    timeProvider.GetUtcNow().ToUniversalTime(),
                    cancellationToken);
            }
        }, cancellationToken);
    }

    private async Task<T> ExecuteSerializableAsync<T>(
        Func<Task<T>> operation,
        CancellationToken cancellationToken)
    {
        if (!dbContext.Database.IsRelational())
        {
            return await operation();
        }

        // Retrying an ambiguous refresh-token commit could replay a one-time operation.
        // Run the transaction inside a non-retrying strategy and let the client retry
        // with the same token, where committed state is re-evaluated from SQL Server.
        var strategy = new OneTimeOperationExecutionStrategy(dbContext);
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken);
            var result = await operation();
            await transaction.CommitAsync(cancellationToken);
            return result;
        });
    }

    private Task ExecuteSerializableAsync(
        Func<Task> operation,
        CancellationToken cancellationToken) =>
        ExecuteSerializableAsync(async () =>
        {
            await operation();
            return true;
        }, cancellationToken);

    private sealed class OneTimeOperationExecutionStrategy(SheetYarDbContext context)
        : ExecutionStrategy(context, maxRetryCount: 0, maxRetryDelay: TimeSpan.Zero)
    {
        protected override bool ShouldRetryOn(Exception exception) => false;
    }

    private static bool IsRefreshRace(Exception exception) => exception switch
    {
        DbUpdateConcurrencyException => true,
        SqlException { Number: 1205 } => true,
        DbUpdateException { InnerException: SqlException { Number: 1205 } } => true,
        _ => false,
    };

    private static void RevokeToken(
        RefreshToken token,
        string reason,
        string? clientIpAddress,
        DateTimeOffset revokedAtUtc)
    {
        token.RevokedAtUtc = revokedAtUtc;
        token.RevocationReason = reason;
        token.RevokedByIp = string.IsNullOrWhiteSpace(clientIpAddress)
            ? null
            : clientIpAddress.Trim()[..Math.Min(clientIpAddress.Trim().Length, 45)];
    }

    private static AuthenticatedUser ToAuthenticatedUser(AppUser user) =>
        new(user.Id, user.Email ?? string.Empty, user.DisplayName);

    private static void ThrowIfIdentityFailed(IdentityResult result)
    {
        if (result.Succeeded)
        {
            return;
        }

        if (result.Errors.Any(error =>
                error.Code is "DuplicateEmail" or "DuplicateUserName"))
        {
            throw new ConflictException("An account with this email address already exists.");
        }

        var errors = result.Errors
            .GroupBy(
                error => error.Code.Contains("Password", StringComparison.OrdinalIgnoreCase)
                    ? "password"
                    : "email",
                StringComparer.Ordinal)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).ToArray(),
                StringComparer.Ordinal);
        throw new RequestValidationException(errors);
    }
}
