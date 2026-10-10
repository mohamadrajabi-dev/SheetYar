using Microsoft.Extensions.Configuration;

namespace SheetYar.Backend.Tests;

internal static class TestAuthenticationConfiguration
{
    public static IReadOnlyDictionary<string, string?> Create(
        IReadOnlyDictionary<string, string?>? overrides = null)
    {
        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            ["Jwt:Issuer"] = "SheetYar.Tests",
            ["Jwt:Audience"] = "SheetYar.Mobile.Tests",
            ["Jwt:SigningKey"] = Convert.ToBase64String(
                Enumerable.Range(1, 32).Select(value => (byte)value).ToArray()),
            ["Jwt:AccessTokenMinutes"] = "10",
            ["Jwt:RefreshTokenDays"] = "30",
            ["Authentication:RateLimits:RegisterPermitLimit"] = "1000",
            ["Authentication:RateLimits:RegisterWindowMinutes"] = "1",
            ["Authentication:RateLimits:LoginPermitLimit"] = "1000",
            ["Authentication:RateLimits:LoginWindowMinutes"] = "1",
            ["Authentication:RateLimits:RefreshPermitLimit"] = "1000",
            ["Authentication:RateLimits:RefreshWindowMinutes"] = "1",
            ["Authentication:RateLimits:AuthenticatedPermitLimit"] = "1000",
            ["Authentication:RateLimits:AuthenticatedWindowMinutes"] = "1",
            ["Logging:LogLevel:Default"] = "Critical",
            ["Logging:LogLevel:Microsoft.Hosting.Lifetime"] = "Critical",
        };

        if (overrides is not null)
        {
            foreach (var (key, value) in overrides)
            {
                values[key] = value;
            }
        }

        return values;
    }

    public static IConfigurationBuilder AddTestAuthentication(
        this IConfigurationBuilder configuration,
        IReadOnlyDictionary<string, string?>? overrides = null) =>
        configuration.AddInMemoryCollection(Create(overrides));
}
