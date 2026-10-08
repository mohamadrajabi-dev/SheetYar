using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SheetYar.Api;
using SheetYar.Api.Validation;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class ApiApplicationTests : IAsyncLifetime
{
    private const string CorrelationHeaderName = "X-Correlation-ID";

    private static readonly IReadOnlyDictionary<HttpStatusCode, string> ExpectedErrorCodes =
        new Dictionary<HttpStatusCode, string>
        {
            [HttpStatusCode.BadRequest] = "bad_request",
            [HttpStatusCode.Unauthorized] = "unauthorized",
            [HttpStatusCode.Forbidden] = "forbidden",
            [HttpStatusCode.NotFound] = "not_found",
            [HttpStatusCode.Conflict] = "conflict",
            [HttpStatusCode.RequestEntityTooLarge] = "payload_too_large",
            [HttpStatusCode.UnprocessableEntity] = "validation_failed",
            [HttpStatusCode.InternalServerError] = "internal_server_error",
        };

    private WebApplication? _application;
    private HttpClient? _client;

    public async Task InitializeAsync()
    {
        _application = ApiApplication.Build(Array.Empty<string>(), "Testing");
        _application.MapGet(
                "/_testing/status/{statusCode:int}",
                static (int statusCode) => Results.StatusCode(statusCode))
            .ExcludeFromDescription();
        _application.MapGet("/_testing/exception", ThrowUnhandledExceptionAsync)
            .ExcludeFromDescription();
        _application.MapPost(
                "/_testing/validation",
                static (ValidationProbeRequest request) => Results.Ok(request))
            .Validate<ValidationProbeRequest>()
            .ExcludeFromDescription();

        _application.Urls.Clear();
        _application.Urls.Add("http://127.0.0.1:0");
        await _application.StartAsync();

        var server = _application.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
        var address = Assert.Single(addresses ?? Array.Empty<string>());
        _client = new HttpClient { BaseAddress = new Uri(address) };
    }

    public async Task DisposeAsync()
    {
        _client?.Dispose();

        if (_application is not null)
        {
            await _application.DisposeAsync();
        }
    }

    [Fact]
    public async Task Health_ReturnsHealthyResponseAndCorrelationHeader()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/health");
        request.Headers.Add(CorrelationHeaderName, "test-health-correlation");

        using var response = await Client.SendAsync(request);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", document.RootElement.GetProperty("status").GetString());
        Assert.Equal(
            "test-health-correlation",
            Assert.Single(response.Headers.GetValues(CorrelationHeaderName)));
    }

    [Fact]
    public async Task OpenApi_DescribesTheHealthEndpoint()
    {
        using var response = await Client.GetAsync("/openapi/v1.json");
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(document.RootElement.GetProperty("paths").TryGetProperty("/health", out _));
    }

    [Fact]
    public async Task RequiredErrorStatuses_UseTheSameProblemContract()
    {
        foreach (var (statusCode, expectedCode) in ExpectedErrorCodes)
        {
            var path = statusCode == HttpStatusCode.NotFound
                ? "/_testing/missing"
                : $"/_testing/status/{(int)statusCode}";
            var correlationId = $"test-{(int)statusCode}-correlation";
            using var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Add(CorrelationHeaderName, correlationId);

            using var response = await Client.SendAsync(request);
            using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            Assert.Equal(statusCode, response.StatusCode);
            Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
            AssertProblemContract(document.RootElement, statusCode, expectedCode, path, correlationId);
            Assert.Equal(correlationId, Assert.Single(response.Headers.GetValues(CorrelationHeaderName)));
        }
    }

    [Fact]
    public async Task UnhandledException_ReturnsSanitizedInternalServerError()
    {
        const string correlationId = "test-exception-correlation";
        using var request = new HttpRequestMessage(HttpMethod.Get, "/_testing/exception");
        request.Headers.Add(CorrelationHeaderName, correlationId);

        using var response = await Client.SendAsync(request);
        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        AssertProblemContract(
            document.RootElement,
            HttpStatusCode.InternalServerError,
            "internal_server_error",
            "/_testing/exception",
            correlationId);
        Assert.DoesNotContain("Sensitive exception details", body, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ValidationFilter_ReturnsValidationErrorsInTheProblemContract()
    {
        const string correlationId = "test-validation-correlation";
        using var request = new HttpRequestMessage(HttpMethod.Post, "/_testing/validation")
        {
            Content = JsonContent.Create(new ValidationProbeRequest(string.Empty)),
        };
        request.Headers.Add(CorrelationHeaderName, correlationId);

        using var response = await Client.SendAsync(request);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        AssertProblemContract(
            document.RootElement,
            HttpStatusCode.UnprocessableEntity,
            "validation_failed",
            "/_testing/validation",
            correlationId);
        Assert.NotEmpty(document.RootElement.GetProperty("errors").EnumerateObject());
    }

    private static Task ThrowUnhandledExceptionAsync(HttpContext context)
    {
        throw new InvalidOperationException("Sensitive exception details must not be returned.");
    }

    private static void AssertProblemContract(
        JsonElement problem,
        HttpStatusCode statusCode,
        string expectedCode,
        string expectedInstance,
        string expectedCorrelationId)
    {
        Assert.False(string.IsNullOrWhiteSpace(problem.GetProperty("type").GetString()));
        Assert.False(string.IsNullOrWhiteSpace(problem.GetProperty("title").GetString()));
        Assert.Equal((int)statusCode, problem.GetProperty("status").GetInt32());
        Assert.False(string.IsNullOrWhiteSpace(problem.GetProperty("detail").GetString()));
        Assert.Equal(expectedInstance, problem.GetProperty("instance").GetString());
        Assert.Equal(expectedCode, problem.GetProperty("code").GetString());
        Assert.Equal(expectedCorrelationId, problem.GetProperty("correlationId").GetString());
        Assert.Equal(JsonValueKind.Object, problem.GetProperty("errors").ValueKind);
    }

    private HttpClient Client => _client ?? throw new InvalidOperationException("The API test host is not initialized.");

    public sealed record ValidationProbeRequest([property: Required] string Name);
}
