namespace SheetYar.Application.Authentication;

public sealed record AuthenticatedUser(
    Guid Id,
    string Email,
    string DisplayName);

public sealed record AuthSession(
    string TokenType,
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAtUtc,
    AuthenticatedUser User);

public interface IAuthenticationService
{
    Task<AuthSession> RegisterAsync(
        string email,
        string password,
        string displayName,
        string? clientIpAddress,
        CancellationToken cancellationToken);

    Task<AuthSession> LoginAsync(
        string email,
        string password,
        string? clientIpAddress,
        CancellationToken cancellationToken);

    Task<AuthSession> RefreshAsync(
        string refreshToken,
        string? clientIpAddress,
        CancellationToken cancellationToken);

    Task LogoutAsync(
        Guid userId,
        string refreshToken,
        string? clientIpAddress,
        CancellationToken cancellationToken);

    Task RevokeAllAsync(
        Guid userId,
        string? clientIpAddress,
        CancellationToken cancellationToken);

    Task<AuthenticatedUser> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken);
}
