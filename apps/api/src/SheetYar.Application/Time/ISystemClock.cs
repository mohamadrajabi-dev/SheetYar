namespace SheetYar.Application.Time;

public interface ISystemClock
{
    DateTimeOffset UtcNow { get; }
}
