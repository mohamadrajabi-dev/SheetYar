using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using SheetYar.Application.Errors;

namespace SheetYar.Api.Errors;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
        {
            logger.LogInformation("HTTP request was canceled by the client");
            if (!httpContext.Response.HasStarted)
            {
                httpContext.Response.StatusCode = 499;
            }

            return true;
        }

        var (statusCode, errors) = MapException(exception);

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception while processing HTTP request");
        }
        else
        {
            logger.LogWarning(exception, "Request failed with status code {StatusCode}", statusCode);
        }

        httpContext.Response.StatusCode = statusCode;
        var problemDetails = ApiProblemDetailsFactory.Create(httpContext, statusCode, errors);

        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
        });

        return true;
    }

    private static (int StatusCode, IReadOnlyDictionary<string, string[]>? Errors) MapException(
        Exception exception) => exception switch
        {
            RequestValidationException validationException =>
                (StatusCodes.Status422UnprocessableEntity, validationException.Errors),
            ConflictException => (StatusCodes.Status409Conflict, null),
            KeyNotFoundException => (StatusCodes.Status404NotFound, null),
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, null),
            BadHttpRequestException badRequestException when
                badRequestException.StatusCode == StatusCodes.Status413PayloadTooLarge =>
                (StatusCodes.Status413PayloadTooLarge, null),
            BadHttpRequestException => (StatusCodes.Status400BadRequest, null),
            JsonException => (StatusCodes.Status400BadRequest, null),
            _ => (StatusCodes.Status500InternalServerError, null),
        };
}
