using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SheetYar.Application.Authentication;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class AuthenticationSqlServerConcurrencyTests
{
    [SqlServerFact]
    [Trait("Category", "SqlServer")]
    public async Task ConcurrentRefresh_RevokesWinningFamilyWithoutInvalidatingLaterSessions()
    {
        var serverConnectionString = Environment.GetEnvironmentVariable(
            SqlServerFactAttribute.ConnectionStringEnvironmentVariable)!;

        await using var host = await SqlServerAuthenticationTestHost.StartAsync(
            serverConnectionString);
        var email = $"sql-race-{Guid.NewGuid():N}@example.com";
        using var registrationResponse = await host.Client.PostAsJsonAsync(
            "/api/v1/auth/register",
            new
            {
                Email = email,
                Password = "Strong!Pass123",
                DisplayName = "SQL Race User",
            });
        Assert.Equal(HttpStatusCode.Created, registrationResponse.StatusCode);
        var original = (await registrationResponse.Content.ReadFromJsonAsync<AuthSession>())!;

        var releaseRefreshRequests = new TaskCompletionSource(
            TaskCreationOptions.RunContinuationsAsynchronously);
        var firstRefreshTask = SendRefreshAfterReleaseAsync();
        var secondRefreshTask = SendRefreshAfterReleaseAsync();
        releaseRefreshRequests.SetResult();
        var responses = await Task.WhenAll(firstRefreshTask, secondRefreshTask);
        try
        {
            Assert.Equal(
                [HttpStatusCode.OK, HttpStatusCode.Unauthorized],
                responses.Select(response => response.StatusCode).Order().ToArray());
            var successfulResponse = Assert.Single(
                responses.Where(response => response.StatusCode == HttpStatusCode.OK));
            var rotated = (await successfulResponse.Content.ReadFromJsonAsync<AuthSession>())!;

            using var rotatedRefreshResponse = await host.Client.PostAsJsonAsync(
                "/api/v1/auth/refresh",
                new { rotated.RefreshToken });
            Assert.Equal(HttpStatusCode.Unauthorized, rotatedRefreshResponse.StatusCode);

            using var loginResponse = await host.Client.PostAsJsonAsync(
                "/api/v1/auth/login",
                new { Email = email, Password = "Strong!Pass123" });
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            var newSession = (await loginResponse.Content.ReadFromJsonAsync<AuthSession>())!;

            using var repeatedReplayResponse = await host.Client.PostAsJsonAsync(
                "/api/v1/auth/refresh",
                new { original.RefreshToken });
            Assert.Equal(HttpStatusCode.Unauthorized, repeatedReplayResponse.StatusCode);

            using var meRequest = new HttpRequestMessage(HttpMethod.Get, "/api/v1/auth/me");
            meRequest.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                newSession.AccessToken);
            using var meResponse = await host.Client.SendAsync(meRequest);
            Assert.Equal(HttpStatusCode.OK, meResponse.StatusCode);
        }
        finally
        {
            foreach (var response in responses)
            {
                response.Dispose();
            }
        }

        async Task<HttpResponseMessage> SendRefreshAfterReleaseAsync()
        {
            await releaseRefreshRequests.Task;
            return await host.Client.PostAsJsonAsync(
                "/api/v1/auth/refresh",
                new { original.RefreshToken });
        }
    }
}

public sealed class SqlServerFactAttribute : FactAttribute
{
    public const string ConnectionStringEnvironmentVariable = "SHEETYAR_TEST_SQLSERVER";

    public SqlServerFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(
                Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariable)))
        {
            Skip = $"Set {ConnectionStringEnvironmentVariable} to run this SQL Server test.";
        }
    }
}
