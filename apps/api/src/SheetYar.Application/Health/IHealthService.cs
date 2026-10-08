namespace SheetYar.Application.Health;

public interface IHealthService
{
    ValueTask<HealthSnapshot> CheckAsync(CancellationToken cancellationToken);
}

public sealed record HealthSnapshot(string Status, DateTimeOffset CheckedAtUtc);
