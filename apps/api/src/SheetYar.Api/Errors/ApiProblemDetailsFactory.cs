using Microsoft.AspNetCore.Mvc;
using SheetYar.Api.Middleware;

namespace SheetYar.Api.Errors;

public static class ApiProblemDetailsFactory
{
    public static ProblemDetails Create(
        HttpContext httpContext,
        int statusCode,
        IReadOnlyDictionary<string, string[]>? errors = null)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
        };

        if (errors is not null)
        {
            problemDetails.Extensions["errors"] = errors;
        }

        Enrich(httpContext, problemDetails);
        return problemDetails;
    }

    public static void Enrich(HttpContext httpContext, ProblemDetails problemDetails)
    {
        var statusCode = problemDetails.Status ?? httpContext.Response.StatusCode;
        var descriptor = ApiErrorCatalog.Get(statusCode);

        problemDetails.Type = $"urn:sheetyar:error:{descriptor.Code}";
        problemDetails.Title = descriptor.Title;
        problemDetails.Status = descriptor.Status;
        problemDetails.Detail = descriptor.Detail;
        problemDetails.Instance = httpContext.Request.Path.HasValue
            ? httpContext.Request.Path.Value
            : "/";
        problemDetails.Extensions["code"] = descriptor.Code;
        problemDetails.Extensions["correlationId"] = CorrelationIdMiddleware.GetCorrelationId(httpContext);

        if (!problemDetails.Extensions.ContainsKey("errors"))
        {
            problemDetails.Extensions["errors"] = new Dictionary<string, string[]>();
        }
    }
}
