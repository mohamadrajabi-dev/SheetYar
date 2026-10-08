using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Files;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class FileAssetConfiguration : IEntityTypeConfiguration<FileAsset>
{
    public void Configure(EntityTypeBuilder<FileAsset> builder)
    {
        builder.ConfigureBaseEntity(
            "FileAssets",
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_FileAssets_SizeBytes_NonNegative",
                    "[SizeBytes] >= 0");
                tableBuilder.HasCheckConstraint(
                    "CK_FileAssets_Kind_Valid",
                    "[Kind] IN (1, 2, 3, 4)");
                tableBuilder.HasCheckConstraint(
                    "CK_FileAssets_Sha256_Valid",
                    "LEN([Sha256]) = 64 AND " +
                    "[Sha256] COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9A-Fa-f]%'");
                tableBuilder.HasCheckConstraint(
                    "CK_FileAssets_StoragePath_Relative",
                    "LEN(LTRIM(RTRIM([StoragePath]))) > 0 AND " +
                    "[StoragePath] NOT LIKE '%..%' AND " +
                    "CHARINDEX(':', [StoragePath]) = 0 AND " +
                    "CHARINDEX('\\', [StoragePath]) = 0 AND " +
                    "LEFT([StoragePath], 1) <> '/'");
            });

        builder.Property(fileAsset => fileAsset.Kind).HasConversion<int>();
        builder.Property(fileAsset => fileAsset.OriginalFileName).HasMaxLength(255).IsRequired();
        builder.Property(fileAsset => fileAsset.StoragePath)
            .HasColumnType("varchar(512)")
            .IsUnicode(false)
            .IsRequired();
        builder.Property(fileAsset => fileAsset.ContentType)
            .HasColumnType("varchar(100)")
            .IsUnicode(false)
            .IsRequired();
        builder.Property(fileAsset => fileAsset.Extension)
            .HasColumnType("varchar(16)")
            .IsUnicode(false)
            .IsRequired();
        builder.Property(fileAsset => fileAsset.Sha256)
            .HasColumnType("varchar(64)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(fileAsset => fileAsset.StoragePath).IsUnique();
        builder.HasIndex(fileAsset => fileAsset.Sha256);
        builder.HasIndex(fileAsset => new { fileAsset.OwnerUserId, fileAsset.CreatedAtUtc })
            .IsDescending(false, true);
        builder.HasIndex(fileAsset => new { fileAsset.WorkbookId, fileAsset.CreatedAtUtc })
            .IsDescending(false, true);

        builder.HasOne(fileAsset => fileAsset.OwnerUser)
            .WithMany(user => user.FileAssets)
            .HasForeignKey(fileAsset => fileAsset.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(fileAsset => fileAsset.Workbook)
            .WithMany(workbook => workbook.FileAssets)
            .HasForeignKey(fileAsset => fileAsset.WorkbookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
