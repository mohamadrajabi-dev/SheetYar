using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SheetYar.Domain.Auditing;
using SheetYar.Domain.Authentication;
using SheetYar.Domain.Common;
using SheetYar.Domain.Files;
using SheetYar.Domain.Templates;
using SheetYar.Domain.Users;
using SheetYar.Domain.Workbooks;

namespace SheetYar.Infrastructure.Persistence;

public sealed class SheetYarDbContext(
    DbContextOptions<SheetYarDbContext> options,
    TimeProvider timeProvider) : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<AppUser> AppUsers => Set<AppUser>();

    public DbSet<Workbook> Workbooks => Set<Workbook>();

    public DbSet<WorkbookVersion> WorkbookVersions => Set<WorkbookVersion>();

    public DbSet<Template> Templates => Set<Template>();

    public DbSet<FileAsset> FileAssets => Set<FileAsset>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        PrepareTrackedEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        PrepareTrackedEntities();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SheetYarDbContext).Assembly);
    }

    private void PrepareTrackedEntities()
    {
        var now = timeProvider.GetUtcNow().ToUniversalTime();

        foreach (var entry in ChangeTracker.Entries<ITrackedEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = entry.Entity.CreatedAtUtc == default
                    ? now
                    : entry.Entity.CreatedAtUtc.ToUniversalTime();
                entry.Entity.UpdatedAtUtc = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(entity => entity.CreatedAtUtc).IsModified = false;
                entry.Entity.UpdatedAtUtc = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified))
            {
                continue;
            }

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(DateTimeOffset) &&
                    property.CurrentValue is DateTimeOffset value)
                {
                    property.CurrentValue = value.ToUniversalTime();
                }
                else if (property.Metadata.ClrType == typeof(DateTimeOffset?) &&
                         property.CurrentValue is DateTimeOffset nullableValue)
                {
                    property.CurrentValue = nullableValue.ToUniversalTime();
                }
            }
        }
    }
}
