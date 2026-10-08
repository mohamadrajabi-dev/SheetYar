using SheetYar.Domain.Common;
using SheetYar.Domain.Users;

namespace SheetYar.Domain.Authentication;

public sealed class RefreshToken : BaseEntity
{
    public Guid AppUserId { get; set; }

    public byte[] TokenHash { get; set; } = [];

    public Guid TokenFamilyId { get; set; }

    public DateTimeOffset ExpiresAtUtc { get; set; }

    public DateTimeOffset? RevokedAtUtc { get; set; }

    public Guid? ReplacedByTokenId { get; set; }

    public string? CreatedByIp { get; set; }

    public string? RevokedByIp { get; set; }

    public string? RevocationReason { get; set; }

    public AppUser AppUser { get; set; } = null!;

    public RefreshToken? ReplacedByToken { get; set; }

    public ICollection<RefreshToken> PreviousTokens { get; } = new List<RefreshToken>();
}
