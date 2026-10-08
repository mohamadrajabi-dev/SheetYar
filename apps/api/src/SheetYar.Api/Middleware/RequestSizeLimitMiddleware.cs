namespace SheetYar.Api.Middleware;

public sealed class RequestSizeLimitMiddleware(RequestDelegate next)
{
    public const long MaxRequestBodySize = 10 * 1024 * 1024;

    public Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.ContentLength is > MaxRequestBodySize)
        {
            httpContext.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            return Task.CompletedTask;
        }

        return next(httpContext);
    }
}
