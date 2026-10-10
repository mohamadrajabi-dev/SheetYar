using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SheetYar.Application.Authentication;
using SheetYar.Domain.Users;
using SheetYar.Infrastructure.Persistence;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class AuthenticationEndpointTests : IAsyncLifetime
{
    private AuthenticationTestHost? _host;

    public async Task InitializeAsync()
    {
        _host = await AuthenticationTestHost.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (_host is not null)
        {
            await _host.DisposeAsync();
        }
    }

    [Fact]
    public async Task Register_HashesPasswordAndReturnsShortLivedTokens()
    {
        var email = UniqueEmail();
        var session = await RegisterAsync(email);

        Assert.Equal("Bearer", session.TokenType);
        Assert.False(string.IsNullOrWhiteSpace(session.AccessToken));
        Assert.False(string.IsNullOrWhiteSpace(session.RefreshToken));
        Assert.InRange(
            session.AccessTokenExpiresAtUtc - DateTimeOffset.UtcNow,
            TimeSpan.FromMinutes(8),
            TimeSpan.FromMinutes(11));
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(session.AccessToken);
        Assert.Equal(SecurityAlgorithms.HmacSha256, jwt.Header.Alg);
        Assert.Equal(
            session.AccessTokenExpiresAtUtc.ToUnixTimeSeconds(),
            long.Parse(
                jwt.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Exp).Value,
                System.Globalization.CultureInfo.InvariantCulture));

        using var scope = Host.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var user = await userManager.FindByEmailAsync(email);
        Assert.NotNull(user);
        Assert.NotEqual("Strong!Pass123", user.PasswordHash);
        Assert.True(await userManager.CheckPasswordAsync(user, "Strong!Pass123"));

        var dbContext = scope.ServiceProvider.GetRequiredService<SheetYarDbContext>();
        var storedToken = await dbContext.RefreshTokens.SingleAsync(token => token.AppUserId == user.Id);
        Assert.Equal(32, storedToken.TokenHash.Length);
        Assert.False(storedToken.TokenHash.SequenceEqual(Encoding.UTF8.GetBytes(session.RefreshToken)));
        Assert.True(storedToken.TokenHash.SequenceEqual(
            SHA256.HashData(Encoding.UTF8.GetBytes(session.RefreshToken))));
    }

    [Fact]
    public async Task Register_WithDuplicateEmailIgnoringCase_ReturnsConflict()
    {
        var email = UniqueEmail();
        await RegisterAsync(email);

        using var response = await Client.PostAsJsonAsync(
            "/api/v1/auth/register",
            new { Email = email.ToUpperInvariant(), Password = "Strong!Pass123", DisplayName = "Second User" });

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        await AssertProblemCodeAsync(response, "conflict");
    }

    [Fact]
    public async Task Login_WithInvalidCredentialsUsesTheSameUnauthorizedContract()
    {
        var email = UniqueEmail();
        await RegisterAsync(email);

        using var wrongPassword = await Client.PostAsJsonAsync(
            "/api/v1/auth/login",
            new { Email = email, Password = "Wrong!Password123" });
        using var unknownUser = await Client.PostAsJsonAsync(
            "/api/v1/auth/login",
            new { Email = UniqueEmail(), Password = "Wrong!Password123" });

        Assert.Equal(HttpStatusCode.Unauthorized, wrongPassword.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, unknownUser.StatusCode);
        var wrongProblem = await ReadJsonAsync(wrongPassword);
        var unknownProblem = await ReadJsonAsync(unknownUser);
        Assert.Equal(wrongProblem.GetProperty("code").GetString(), unknownProblem.GetProperty("code").GetString());
        Assert.Equal(wrongProblem.GetProperty("detail").GetString(), unknownProblem.GetProperty("detail").GetString());
    }

    [Fact]
    public async Task Login_LocksAccountAfterFiveFailedAttempts()
    {
        var email = UniqueEmail();
        await RegisterAsync(email);

        for (var attempt = 0; attempt < 5; attempt++)
        {
            using var failure = await Client.PostAsJsonAsync(
                "/api/v1/auth/login",
                new { Email = email, Password = "Wrong!Password123" });
            Assert.Equal(HttpStatusCode.Unauthorized, failure.StatusCode);
        }

        using var lockedLogin = await Client.PostAsJsonAsync(
            "/api/v1/auth/login",
            new { Email = email, Password = "Strong!Pass123" });
        Assert.Equal(HttpStatusCode.Unauthorized, lockedLogin.StatusCode);

        using var scope = Host.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var user = await userManager.FindByEmailAsync(email);
        Assert.NotNull(user?.LockoutEnd);
        Assert.True(user.LockoutEnd > DateTimeOffset.UtcNow);
    }

    [Fact]
    public async Task Refresh_RotatesTokenAndReuseRevokesTheFamily()
    {
        var original = await RegisterAsync(UniqueEmail());
        var rotated = await RefreshAsync(original.RefreshToken, HttpStatusCode.OK);
        Assert.NotNull(rotated);
        Assert.NotEqual(original.RefreshToken, rotated.RefreshToken);

        await RefreshAsync(original.RefreshToken, HttpStatusCode.Unauthorized);
        await RefreshAsync(rotated.RefreshToken, HttpStatusCode.Unauthorized);

        using var scope = Host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SheetYarDbContext>();
        var tokens = await dbContext.RefreshTokens
            .Where(token => token.AppUserId == original.User.Id)
            .ToListAsync();
        Assert.Equal(2, tokens.Count);
        Assert.All(tokens, token => Assert.NotNull(token.RevokedAtUtc));
        Assert.Single(tokens, token => token.ReplacedByTokenId is not null);
        Assert.Single(tokens.Select(token => token.TokenFamilyId).Distinct());
    }

    [Fact]
    public async Task Refresh_RepeatedReplayOfCompromisedFamilyDoesNotInvalidateNewSession()
    {
        var email = UniqueEmail();
        var original = await RegisterAsync(email);
        var rotated = await RefreshAsync(original.RefreshToken, HttpStatusCode.OK);
        Assert.NotNull(rotated);
        await RefreshAsync(original.RefreshToken, HttpStatusCode.Unauthorized);

        var newSession = await LoginAsync(email, "Strong!Pass123");
        await RefreshAsync(original.RefreshToken, HttpStatusCode.Unauthorized);

        using var meRequest = CreateAuthorizedRequest(
            HttpMethod.Get,
            "/api/v1/auth/me",
            newSession.AccessToken);
        using var meResponse = await Client.SendAsync(meRequest);
        Assert.Equal(HttpStatusCode.OK, meResponse.StatusCode);
        Assert.NotNull(await RefreshAsync(newSession.RefreshToken, HttpStatusCode.OK));
    }

    [Fact]
    public async Task Logout_RevokesOnlyThePresentedRefreshToken()
    {
        var firstSession = await RegisterAsync(UniqueEmail());
        var secondSession = await LoginAsync(firstSession.User.Email, "Strong!Pass123");

        using var logout = CreateAuthorizedRequest(
            HttpMethod.Post,
            "/api/v1/auth/logout",
            firstSession.AccessToken,
            JsonContent.Create(new { firstSession.RefreshToken }));
        using var logoutResponse = await Client.SendAsync(logout);
        Assert.Equal(HttpStatusCode.NoContent, logoutResponse.StatusCode);

        await RefreshAsync(firstSession.RefreshToken, HttpStatusCode.Unauthorized);
        var refreshedSecondSession = await RefreshAsync(secondSession.RefreshToken, HttpStatusCode.OK);
        Assert.NotNull(refreshedSecondSession);
    }

    [Fact]
    public async Task RevokeAll_InvalidatesRefreshAndExistingAccessTokens()
    {
        var session = await RegisterAsync(UniqueEmail());
        var secondSession = await LoginAsync(session.User.Email, "Strong!Pass123");
        using var revokeRequest = CreateAuthorizedRequest(
            HttpMethod.Post,
            "/api/v1/auth/revoke-all",
            session.AccessToken);
        using var revokeResponse = await Client.SendAsync(revokeRequest);
        Assert.Equal(HttpStatusCode.NoContent, revokeResponse.StatusCode);

        await RefreshAsync(session.RefreshToken, HttpStatusCode.Unauthorized);
        await RefreshAsync(secondSession.RefreshToken, HttpStatusCode.Unauthorized);
        using var meRequest = CreateAuthorizedRequest(HttpMethod.Get, "/api/v1/auth/me", session.AccessToken);
        using var meResponse = await Client.SendAsync(meRequest);
        Assert.Equal(HttpStatusCode.Unauthorized, meResponse.StatusCode);
        using var secondMeRequest = CreateAuthorizedRequest(
            HttpMethod.Get,
            "/api/v1/auth/me",
            secondSession.AccessToken);
        using var secondMeResponse = await Client.SendAsync(secondMeRequest);
        Assert.Equal(HttpStatusCode.Unauthorized, secondMeResponse.StatusCode);
    }

    [Fact]
    public async Task Me_ReturnsOnlyTheAuthenticatedUserProfile()
    {
        var session = await RegisterAsync(UniqueEmail());
        using var request = CreateAuthorizedRequest(HttpMethod.Get, "/api/v1/auth/me", session.AccessToken);
        using var response = await Client.SendAsync(request);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(session.User.Email, body, StringComparison.Ordinal);
        Assert.DoesNotContain("passwordHash", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("tokenHash", body, StringComparison.OrdinalIgnoreCase);

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(session.AccessToken);
        Assert.Equal(session.User.Id.ToString("D"), jwt.Subject);
        Assert.False(string.IsNullOrWhiteSpace(jwt.Id));
        Assert.Equal(session.User.Email, jwt.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Email).Value);
    }

    private async Task<AuthSession> RegisterAsync(string email)
    {
        using var response = await Client.PostAsJsonAsync(
            "/api/v1/auth/register",
            new { Email = email, Password = "Strong!Pass123", DisplayName = "SheetYar User" });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        return (await response.Content.ReadFromJsonAsync<AuthSession>())!;
    }

    private async Task<AuthSession> LoginAsync(string email, string password)
    {
        using var response = await Client.PostAsJsonAsync(
            "/api/v1/auth/login",
            new { Email = email, Password = password });
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return (await response.Content.ReadFromJsonAsync<AuthSession>())!;
    }

    private async Task<AuthSession?> RefreshAsync(string refreshToken, HttpStatusCode expectedStatus)
    {
        using var response = await Client.PostAsJsonAsync(
            "/api/v1/auth/refresh",
            new { RefreshToken = refreshToken });
        Assert.Equal(expectedStatus, response.StatusCode);
        return expectedStatus == HttpStatusCode.OK
            ? await response.Content.ReadFromJsonAsync<AuthSession>()
            : null;
    }

    private static HttpRequestMessage CreateAuthorizedRequest(
        HttpMethod method,
        string path,
        string accessToken,
        HttpContent? content = null) =>
        new(method, path)
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", accessToken) },
            Content = content,
        };

    private static async Task AssertProblemCodeAsync(HttpResponseMessage response, string expectedCode)
    {
        var problem = await ReadJsonAsync(response);
        Assert.Equal(expectedCode, problem.GetProperty("code").GetString());
        Assert.False(string.IsNullOrWhiteSpace(problem.GetProperty("correlationId").GetString()));
    }

    private static async Task<JsonElement> ReadJsonAsync(HttpResponseMessage response)
    {
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.Clone();
    }

    private static string UniqueEmail() => $"user-{Guid.NewGuid():N}@example.com";

    private AuthenticationTestHost Host =>
        _host ?? throw new InvalidOperationException("The authentication test host is not initialized.");

    private HttpClient Client => Host.Client;
}
