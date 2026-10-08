using SheetYar.Application.Time;
using SheetYar.Infrastructure.Health;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class HealthServiceTests
{
    [Fact]
    public async Task CheckAsync_StopsWhenCancellationIsRequested()
    {
        var service = new HealthService(new StubSystemClock());
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await service.CheckAsync(cancellationSource.Token));
    }

    private sealed class StubSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow { get; } = DateTimeOffset.UnixEpoch;
    }
}
