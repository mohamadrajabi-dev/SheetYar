using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Workbooks;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class WorkbookConfiguration : IEntityTypeConfiguration<Workbook>
{
    public void Configure(EntityTypeBuilder<Workbook> builder)
    {
        builder.ConfigureBaseEntity("Workbooks");

        builder.Property(workbook => workbook.Name).HasMaxLength(200).IsRequired();
        builder.Property(workbook => workbook.Description).HasMaxLength(1000);

        builder.HasIndex(workbook => new { workbook.OwnerUserId, workbook.UpdatedAtUtc })
            .IsDescending(false, true);
        builder.HasIndex(workbook => workbook.SourceTemplateId);

        builder.HasOne(workbook => workbook.OwnerUser)
            .WithMany(user => user.Workbooks)
            .HasForeignKey(workbook => workbook.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(workbook => workbook.SourceTemplate)
            .WithMany(template => template.Workbooks)
            .HasForeignKey(workbook => workbook.SourceTemplateId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
