using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Templates;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ConfigureBaseEntity(
            "Templates",
            tableBuilder => tableBuilder.HasCheckConstraint(
                "CK_Templates_SortOrder_NonNegative",
                "[SortOrder] >= 0"));

        builder.Property(template => template.Code).HasMaxLength(80).IsRequired();
        builder.Property(template => template.Name).HasMaxLength(160).IsRequired();
        builder.Property(template => template.Description).HasMaxLength(1000);

        builder.HasIndex(template => template.Code).IsUnique();
        builder.HasIndex(template => new { template.IsActive, template.SortOrder });
        builder.HasIndex(template => template.SourceFileAssetId)
            .IsUnique()
            .HasFilter("[SourceFileAssetId] IS NOT NULL");

        builder.HasOne(template => template.SourceFileAsset)
            .WithOne(fileAsset => fileAsset.SourceForTemplate)
            .HasForeignKey<Template>(template => template.SourceFileAssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
