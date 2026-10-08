using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Auditing;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ConfigureBaseEntity(
            "AuditLogs",
            tableBuilder => tableBuilder.HasCheckConstraint(
                "CK_AuditLogs_DetailsJson_Valid",
                "[DetailsJson] IS NULL OR ISJSON([DetailsJson]) = 1"));

        builder.Property(auditLog => auditLog.Action).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.EntityType).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.CorrelationId)
            .HasColumnType("varchar(128)")
            .IsUnicode(false);
        builder.Property(auditLog => auditLog.IpAddress)
            .HasColumnType("varchar(45)")
            .IsUnicode(false);
        builder.Property(auditLog => auditLog.DetailsJson).HasColumnType("nvarchar(max)");

        builder.HasIndex(auditLog => auditLog.CreatedAtUtc).IsDescending(true);
        builder.HasIndex(auditLog => new { auditLog.ActorUserId, auditLog.CreatedAtUtc })
            .IsDescending(false, true);
        builder.HasIndex(auditLog => new
        {
            auditLog.EntityType,
            auditLog.EntityId,
            auditLog.CreatedAtUtc,
        })
            .IsDescending(false, false, true);

        builder.HasOne(auditLog => auditLog.ActorUser)
            .WithMany(user => user.AuditLogs)
            .HasForeignKey(auditLog => auditLog.ActorUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
