using SheetYar.Domain.Common;
using SheetYar.Domain.Files;
using SheetYar.Domain.Users;

namespace SheetYar.Domain.Workbooks;

public sealed class WorkbookVersion : BaseEntity
{
    public Guid WorkbookId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid SnapshotFileAssetId { get; set; }

    public int VersionNumber { get; set; }

    public string? Label { get; set; }

    public string? ChangeSummary { get; set; }

    public Workbook Workbook { get; set; } = null!;

    public AppUser CreatedByUser { get; set; } = null!;

    public FileAsset SnapshotFileAsset { get; set; } = null!;
}
