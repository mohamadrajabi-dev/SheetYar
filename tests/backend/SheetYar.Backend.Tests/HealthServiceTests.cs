using SheetYar.Application.Health;
using SheetYar.Application.Time;
using SheetYar.Infrastructure.Health;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class HealthServiceTests
{
    [Theory]
    [InlineData(true, "Healthy")]
    [InlineData(false, "Unhealthy")]
    public async Task CheckAsync_ReportsDatabaseState(bool databaseIsHealthy, string expectedStatus)
    {
        var databaseHealthService = new StubDatabaseHealthService(databaseIsHealthy);
        var service = new HealthService(new StubSystemClock(), databaseHealthService);
        using var cancellationSource = new CancellationTokenSource();

        var result = await service.CheckAsync(cancellationSource.Token);

        Assert.Equal(expectedStatus, result.Status);
        Assert.Equal(expectedStatus, result.DatabaseStatus);
        Assert.Equal(DateTimeOffset.UnixEpoch, result.CheckedAtUtc);
        Assert.Equal(cancellationSource.Token, databaseHealthService.ObservedCancellationToken);
    }

    [Fact]
    public async Task CheckAsync_StopsWhenCancellationIsRequested()
    {
        var service = new HealthService(new StubSystemClock(), new StubDatabaseHealthService(true));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await service.CheckAsync(cancellationSource.Token));
    }

    private sealed class StubSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow { get; } = DateTimeOffset.UnixEpoch;
    }

    private sealed class StubDatabaseHealthService(bool isHealthy) : IDatabaseHealthService
    {
        public CancellationToken ObservedCancellationToken { get; private set; }

        public ValueTask<bool> CanConnectAsync(CancellationToken cancellationToken)
        {
            ObservedCancellationToken = cancellationToken;
            cancellationToken.ThrowIfCancellationRequested();
            return ValueTask.FromResult(isHealthy);
        }
    }
}
