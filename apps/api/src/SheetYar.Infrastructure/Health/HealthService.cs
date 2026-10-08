using SheetYar.Application.Health;
using SheetYar.Application.Time;

namespace SheetYar.Infrastructure.Health;

public sealed class HealthService(
    ISystemClock systemClock,
    IDatabaseHealthService databaseHealthService) : IHealthService
{
    public async ValueTask<HealthSnapshot> CheckAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var databaseIsHealthy = await databaseHealthService.CanConnectAsync(cancellationToken);

        return new HealthSnapshot(
            databaseIsHealthy ? "Healthy" : "Unhealthy",
            systemClock.UtcNow,
            databaseIsHealthy ? "Healthy" : "Unhealthy");
    }
}
