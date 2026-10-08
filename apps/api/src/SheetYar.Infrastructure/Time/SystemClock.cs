using SheetYar.Application.Time;

namespace SheetYar.Infrastructure.Time;

public sealed class SystemClock(TimeProvider timeProvider) : ISystemClock
{
    public DateTimeOffset UtcNow => timeProvider.GetUtcNow().ToUniversalTime();
}
