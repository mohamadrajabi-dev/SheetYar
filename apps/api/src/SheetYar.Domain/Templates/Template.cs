using SheetYar.Domain.Common;
using SheetYar.Domain.Files;
using SheetYar.Domain.Workbooks;

namespace SheetYar.Domain.Templates;

public sealed class Template : BaseEntity
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; }

    public Guid? SourceFileAssetId { get; set; }

    public FileAsset? SourceFileAsset { get; set; }

    public ICollection<Workbook> Workbooks { get; } = new List<Workbook>();
}
