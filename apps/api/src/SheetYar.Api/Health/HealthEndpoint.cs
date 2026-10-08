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
                    return Results.Ok(new HealthResponse(health.Status, health.CheckedAtUtc));
                })
            .WithName("Health")
            .WithTags("System")
            .AllowAnonymous()
            .Produces<HealthResponse>(StatusCodes.Status200OK)
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

public sealed record HealthResponse(string Status, DateTimeOffset CheckedAtUtc);
