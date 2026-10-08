using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Common;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal static class EntityTypeBuilderExtensions
{
    public static void ConfigureBaseEntity<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        string tableName,
        Action<TableBuilder<TEntity>>? configureTable = null)
        where TEntity : BaseEntity
    {
        builder.ToTable(
            tableName,
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    $"CK_{tableName}_CreatedAtUtc_UTC",
                    "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                tableBuilder.HasCheckConstraint(
                    $"CK_{tableName}_UpdatedAtUtc_UTC",
                    "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                tableBuilder.HasCheckConstraint(
                    $"CK_{tableName}_UpdatedAtUtc_NotBeforeCreated",
                    "[UpdatedAtUtc] >= [CreatedAtUtc]");
                configureTable?.Invoke(tableBuilder);
            });

        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedNever();
        builder.Property(entity => entity.CreatedAtUtc).HasColumnType("datetimeoffset(7)").IsRequired();
        builder.Property(entity => entity.UpdatedAtUtc).HasColumnType("datetimeoffset(7)").IsRequired();
        builder.Property(entity => entity.RowVersion).IsRowVersion().IsConcurrencyToken();
    }
}
