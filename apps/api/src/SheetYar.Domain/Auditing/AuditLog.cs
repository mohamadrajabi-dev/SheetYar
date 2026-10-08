using SheetYar.Domain.Common;
using SheetYar.Domain.Users;

namespace SheetYar.Domain.Auditing;

public sealed class AuditLog : BaseEntity
{
    public Guid? ActorUserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityType { get; set; } = string.Empty;

    public Guid? EntityId { get; set; }

    public string? CorrelationId { get; set; }

    public string? IpAddress { get; set; }

    public string? DetailsJson { get; set; }

    public AppUser? ActorUser { get; set; }
}
