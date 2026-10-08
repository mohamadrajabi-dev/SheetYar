using SheetYar.Api.Errors;
using SheetYar.Application.Health;

namespace SheetYar.Api.Health;

public static class HealthEndpoint
{
    public static IEndpointRouteBuilder MapHealthEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                "/health",
                async (IHealthService healthService, CancellationToken cancellationToken) =>
                {
                    var health = await healthService.CheckAsync(cancellationToken);
                    var response = new HealthResponse(
                        health.Status,
                        health.CheckedAtUtc,
                        health.DatabaseStatus);

                    return health.Status == "Healthy"
                        ? Results.Ok(response)
                        : Results.Json(response, statusCode: StatusCodes.Status503ServiceUnavailable);
                })
            .WithName("Health")
            .WithTags("System")
            .AllowAnonymous()
            .Produces<HealthResponse>(StatusCodes.Status200OK)
            .Produces<HealthResponse>(StatusCodes.Status503ServiceUnavailable)
            .Produces<ApiProblemResponse>(StatusCodes.Status400BadRequest, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status401Unauthorized, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status403Forbidden, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status404NotFound, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status409Conflict, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status413PayloadTooLarge, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status422UnprocessableEntity, "application/problem+json")
            .Produces<ApiProblemResponse>(StatusCodes.Status500InternalServerError, "application/problem+json");

        return endpoints;
    }
}

public sealed record HealthResponse(
    string Status,
    DateTimeOffset CheckedAtUtc,
    string DatabaseStatus);
