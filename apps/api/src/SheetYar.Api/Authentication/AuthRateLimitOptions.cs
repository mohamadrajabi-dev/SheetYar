namespace SheetYar.Api.Authentication;

public sealed class AuthRateLimitOptions
{
    public const string SectionName = "Authentication:RateLimits";

    public int RegisterPermitLimit { get; set; } = 3;

    public int RegisterWindowMinutes { get; set; } = 15;

    public int LoginPermitLimit { get; set; } = 5;

    public int LoginWindowMinutes { get; set; } = 1;

    public int RefreshPermitLimit { get; set; } = 10;

    public int RefreshWindowMinutes { get; set; } = 1;

    public int AuthenticatedPermitLimit { get; set; } = 20;

    public int AuthenticatedWindowMinutes { get; set; } = 1;
}

internal static class AuthRateLimitPolicies
{
    public const string Register = "auth-register";
    public const string Login = "auth-login";
    public const string Refresh = "auth-refresh";
    public const string Authenticated = "auth-authenticated";
}
