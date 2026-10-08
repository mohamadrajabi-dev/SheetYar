using System.Diagnostics;

namespace SheetYar.Api.Middleware;

public sealed class CorrelationIdMiddleware(
    RequestDelegate next,
    ILogger<CorrelationIdMiddleware> logger)
{
    public const string HeaderName = "X-Correlation-ID";
    private const int MaximumLength = 128;
    private static readonly object ItemKey = new();

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var correlationId = ResolveCorrelationId(httpContext);
        httpContext.TraceIdentifier = correlationId;
        httpContext.Items[ItemKey] = correlationId;
        httpContext.Response.Headers[HeaderName] = correlationId;

        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
        });

        var startedAt = Stopwatch.GetTimestamp();
        logger.LogInformation(
            "HTTP request started {RequestMethod} {RequestPath}",
            httpContext.Request.Method,
            httpContext.Request.Path);

        try
        {
            await next(httpContext);
        }
        finally
        {
            logger.LogInformation(
                "HTTP request completed {RequestMethod} {RequestPath} with {StatusCode} in {ElapsedMilliseconds} ms",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.Response.StatusCode,
                Stopwatch.GetElapsedTime(startedAt).TotalMilliseconds);
        }
    }

    public static string GetCorrelationId(HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue(ItemKey, out var value) && value is string correlationId)
        {
            return correlationId;
        }

        if (!string.IsNullOrWhiteSpace(httpContext.TraceIdentifier))
        {
            return httpContext.TraceIdentifier;
        }

        return Guid.NewGuid().ToString("N");
    }

    private static string ResolveCorrelationId(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue(HeaderName, out var values) && values.Count == 1)
        {
            var candidate = values[0];
            if (IsValid(candidate))
            {
                return candidate!;
            }
        }

        return Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString("N");
    }

    private static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MaximumLength)
        {
            return false;
        }

        foreach (var character in value)
        {
            if (!char.IsAsciiLetterOrDigit(character) && character is not '-' and not '_' and not '.')
            {
                return false;
            }
        }

        return true;
    }
}
