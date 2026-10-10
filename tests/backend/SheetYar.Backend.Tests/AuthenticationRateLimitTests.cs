using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class AuthenticationRateLimitTests
{
    [Fact]
    public async Task Login_WhenLimitIsExceeded_ReturnsSharedProblemContractAndRetryAfter()
    {
        await using var host = await AuthenticationTestHost.StartAsync(
            new Dictionary<string, string?>
            {
                ["Authentication:RateLimits:LoginPermitLimit"] = "1",
                ["Authentication:RateLimits:LoginWindowMinutes"] = "1",
            });

        using var firstResponse = await host.Client.PostAsJsonAsync(
            "/api/v1/auth/login",
            new { Email = "missing@example.com", Password = "Wrong!Password123" });
        using var secondResponse = await host.Client.PostAsJsonAsync(
            "/api/v1/auth/login",
            new { Email = "missing@example.com", Password = "Wrong!Password123" });
        using var document = JsonDocument.Parse(await secondResponse.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.Unauthorized, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.TooManyRequests, secondResponse.StatusCode);
        Assert.Equal("application/problem+json", secondResponse.Content.Headers.ContentType?.MediaType);
        Assert.Equal("rate_limit_exceeded", document.RootElement.GetProperty("code").GetString());
        Assert.False(string.IsNullOrWhiteSpace(document.RootElement.GetProperty("correlationId").GetString()));
        Assert.True(secondResponse.Headers.RetryAfter is not null || secondResponse.Headers.Contains("Retry-After"));
    }

    [Fact]
    public async Task Me_WhenAuthenticatedLimitIsExceeded_ReturnsTooManyRequests()
    {
        await using var host = await AuthenticationTestHost.StartAsync(
            new Dictionary<string, string?>
            {
                ["Authentication:RateLimits:AuthenticatedPermitLimit"] = "1",
                ["Authentication:RateLimits:AuthenticatedWindowMinutes"] = "1",
            });
        using var registrationResponse = await host.Client.PostAsJsonAsync(
            "/api/v1/auth/register",
            new
            {
                Email = "rate-limited-user@example.com",
                Password = "Valid!Password123",
                DisplayName = "Rate Limited User",
            });
        using var registration = JsonDocument.Parse(
            await registrationResponse.Content.ReadAsStringAsync());
        var accessToken = registration.RootElement.GetProperty("accessToken").GetString();
        host.Client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        using var firstResponse = await host.Client.GetAsync("/api/v1/auth/me");
        using var secondResponse = await host.Client.GetAsync("/api/v1/auth/me");

        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.TooManyRequests, secondResponse.StatusCode);
    }
}
