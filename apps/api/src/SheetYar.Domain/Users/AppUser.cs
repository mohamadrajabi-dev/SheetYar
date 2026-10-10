using Microsoft.AspNetCore.Identity;
using SheetYar.Domain.Auditing;
using SheetYar.Domain.Authentication;
using SheetYar.Domain.Common;
using SheetYar.Domain.Files;
using SheetYar.Domain.Workbooks;

namespace SheetYar.Domain.Users;

public sealed class AppUser : IdentityUser<Guid>, ITrackedEntity
{
    public AppUser()
    {
        Id = Guid.NewGuid();
    }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = [];

    public string DisplayName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<Workbook> Workbooks { get; } = new List<Workbook>();

    public ICollection<WorkbookVersion> CreatedWorkbookVersions { get; } = new List<WorkbookVersion>();

    public ICollection<FileAsset> FileAssets { get; } = new List<FileAsset>();

    public ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();

    public ICollection<AuditLog> AuditLogs { get; } = new List<AuditLog>();
}
