namespace SheetYar.Application.Health;

public interface IDatabaseHealthService
{
    ValueTask<bool> CanConnectAsync(CancellationToken cancellationToken);
}
