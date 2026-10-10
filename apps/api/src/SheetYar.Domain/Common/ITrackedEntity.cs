namespace SheetYar.Domain.Common;

public interface ITrackedEntity
{
    Guid Id { get; set; }

    DateTimeOffset CreatedAtUtc { get; set; }

    DateTimeOffset UpdatedAtUtc { get; set; }

    byte[] RowVersion { get; set; }
}
