using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SheetYar.Infrastructure.Persistence;

public static class DatabaseInitializationExtensions
{
    public static async Task MigrateAndSeedSheetYarDatabaseAsync(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SheetYarDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        var databaseSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await databaseSeeder.SeedAsync(cancellationToken);
    }
}
