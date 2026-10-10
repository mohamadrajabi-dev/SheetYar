using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using SheetYar.Domain.Auditing;
using SheetYar.Domain.Authentication;
using SheetYar.Domain.Common;
using SheetYar.Domain.Files;
using SheetYar.Domain.Templates;
using SheetYar.Domain.Users;
using SheetYar.Domain.Workbooks;
using SheetYar.Infrastructure.Persistence;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class PersistenceModelTests
{
    private static readonly Type[] EntityTypes =
    [
        typeof(AppUser),
        typeof(Workbook),
        typeof(WorkbookVersion),
        typeof(Template),
        typeof(FileAsset),
        typeof(RefreshToken),
        typeof(AuditLog),
    ];

    private static readonly Type[] IdentityEntityTypes =
    [
        typeof(IdentityRole<Guid>),
        typeof(IdentityRoleClaim<Guid>),
        typeof(IdentityUserClaim<Guid>),
        typeof(IdentityUserLogin<Guid>),
        typeof(IdentityUserRole<Guid>),
        typeof(IdentityUserToken<Guid>),
    ];

    [Fact]
    public void Model_MapsAllRequiredEntitiesWithCommonConcurrencyAndUtcProperties()
    {
        using var dbContext = CreateDbContext();
        var mappedTypes = dbContext.Model.GetEntityTypes()
            .Select(entityType => entityType.ClrType)
            .OrderBy(type => type.FullName, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(
            EntityTypes
                .Concat(IdentityEntityTypes)
                .OrderBy(type => type.FullName, StringComparer.Ordinal),
            mappedTypes);

        foreach (var clrType in EntityTypes)
        {
            var entityType = Assert.IsAssignableFrom<IEntityType>(dbContext.Model.FindEntityType(clrType));
            var primaryKey = Assert.IsAssignableFrom<IKey>(entityType.FindPrimaryKey());
            var idProperty = Assert.Single(primaryKey.Properties);
            Assert.Equal(nameof(BaseEntity.Id), idProperty.Name);
            Assert.Equal(typeof(Guid), idProperty.ClrType);
            Assert.Equal(ValueGenerated.Never, idProperty.ValueGenerated);

            Assert.Equal("datetimeoffset(7)", entityType.FindProperty(nameof(BaseEntity.CreatedAtUtc))?.GetColumnType());
            Assert.Equal("datetimeoffset(7)", entityType.FindProperty(nameof(BaseEntity.UpdatedAtUtc))?.GetColumnType());

            var rowVersion = Assert.IsAssignableFrom<IProperty>(entityType.FindProperty(nameof(BaseEntity.RowVersion)));
            Assert.True(rowVersion.IsConcurrencyToken);
            Assert.Equal(ValueGenerated.OnAddOrUpdate, rowVersion.ValueGenerated);
        }
    }

    [Fact]
    public void FileAsset_StoresMetadataAndSafeRelativePathOnly()
    {
        using var dbContext = CreateDbContext();
        var designTimeModel = dbContext.GetService<IDesignTimeModel>().Model;
        var entityType = Assert.IsAssignableFrom<IEntityType>(designTimeModel.FindEntityType(typeof(FileAsset)));
        var binaryProperties = entityType.GetProperties()
            .Where(property => property.ClrType == typeof(byte[]))
            .Select(property => property.Name)
            .ToArray();

        Assert.Equal([nameof(BaseEntity.RowVersion)], binaryProperties);
        Assert.NotNull(entityType.FindProperty(nameof(FileAsset.StoragePath)));
        Assert.Contains(
            entityType.GetCheckConstraints(),
            constraint => constraint.Name == "CK_FileAssets_StoragePath_Relative");
    }

    [Fact]
    public void Model_DefinesRequiredUniqueBusinessIndexes()
    {
        using var dbContext = CreateDbContext();

        AssertUniqueIndex<AppUser>(dbContext, nameof(AppUser.NormalizedEmail));
        AssertUniqueIndex<Template>(dbContext, nameof(Template.Code));
        AssertUniqueIndex<FileAsset>(dbContext, nameof(FileAsset.StoragePath));
        AssertUniqueIndex<RefreshToken>(dbContext, nameof(RefreshToken.TokenHash));
        AssertUniqueIndex<RefreshToken>(dbContext, nameof(RefreshToken.ReplacedByTokenId));
        AssertUniqueIndex<WorkbookVersion>(
            dbContext,
            nameof(WorkbookVersion.WorkbookId),
            nameof(WorkbookVersion.VersionNumber));
    }

    private static SheetYarDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<SheetYarDbContext>()
            .UseSqlServer()
            .Options;

        return new SheetYarDbContext(options, TimeProvider.System);
    }

    private static void AssertUniqueIndex<TEntity>(
        SheetYarDbContext dbContext,
        params string[] propertyNames)
    {
        var entityType = Assert.IsAssignableFrom<IEntityType>(dbContext.Model.FindEntityType(typeof(TEntity)));
        Assert.Contains(
            entityType.GetIndexes(),
            index => index.IsUnique &&
                     index.Properties.Select(property => property.Name).SequenceEqual(propertyNames));
    }
}
