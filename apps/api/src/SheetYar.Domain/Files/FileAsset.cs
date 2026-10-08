using SheetYar.Domain.Common;
using SheetYar.Domain.Templates;
using SheetYar.Domain.Users;
using SheetYar.Domain.Workbooks;

namespace SheetYar.Domain.Files;

public sealed class FileAsset : BaseEntity
{
    public Guid? OwnerUserId { get; set; }

    public Guid? WorkbookId { get; set; }

    public FileAssetKind Kind { get; set; }

    public string OriginalFileName { get; set; } = string.Empty;

    public string StoragePath { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;

    public long SizeBytes { get; set; }

    public string Sha256 { get; set; } = string.Empty;

    public AppUser? OwnerUser { get; set; }

    public Workbook? Workbook { get; set; }

    public WorkbookVersion? SnapshotForVersion { get; set; }

    public Template? SourceForTemplate { get; set; }
}
