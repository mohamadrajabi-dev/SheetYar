using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Workbooks;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class WorkbookVersionConfiguration : IEntityTypeConfiguration<WorkbookVersion>
{
    public void Configure(EntityTypeBuilder<WorkbookVersion> builder)
    {
        builder.ConfigureBaseEntity(
            "WorkbookVersions",
            tableBuilder => tableBuilder.HasCheckConstraint(
                "CK_WorkbookVersions_VersionNumber_Positive",
                "[VersionNumber] > 0"));

        builder.Property(version => version.Label).HasMaxLength(200);
        builder.Property(version => version.ChangeSummary).HasMaxLength(2000);

        builder.HasIndex(version => new { version.WorkbookId, version.VersionNumber }).IsUnique();
        builder.HasIndex(version => version.SnapshotFileAssetId).IsUnique();
        builder.HasIndex(version => new { version.WorkbookId, version.CreatedAtUtc })
            .IsDescending(false, true);

        builder.HasOne(version => version.Workbook)
            .WithMany(workbook => workbook.Versions)
            .HasForeignKey(version => version.WorkbookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(version => version.CreatedByUser)
            .WithMany(user => user.CreatedWorkbookVersions)
            .HasForeignKey(version => version.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(version => version.SnapshotFileAsset)
            .WithOne(fileAsset => fileAsset.SnapshotForVersion)
            .HasForeignKey<WorkbookVersion>(version => version.SnapshotFileAssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
