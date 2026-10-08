using SheetYar.Application.Health;
using SheetYar.Application.Time;

namespace SheetYar.Infrastructure.Health;

public sealed class HealthService(ISystemClock systemClock) : IHealthService
{
    public ValueTask<HealthSnapshot> CheckAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return ValueTask.FromResult(new HealthSnapshot("Healthy", systemClock.UtcNow));
    }
}
