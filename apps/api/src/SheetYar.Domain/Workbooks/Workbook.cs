using SheetYar.Domain.Common;
using SheetYar.Domain.Files;
using SheetYar.Domain.Templates;
using SheetYar.Domain.Users;

namespace SheetYar.Domain.Workbooks;

public sealed class Workbook : BaseEntity
{
    public Guid OwnerUserId { get; set; }

    public Guid? SourceTemplateId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public AppUser OwnerUser { get; set; } = null!;

    public Template? SourceTemplate { get; set; }

    public ICollection<WorkbookVersion> Versions { get; } = new List<WorkbookVersion>();

    public ICollection<FileAsset> FileAssets { get; } = new List<FileAsset>();
}
