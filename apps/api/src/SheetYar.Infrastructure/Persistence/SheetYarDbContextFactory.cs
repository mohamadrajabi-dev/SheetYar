using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SheetYar.Infrastructure.Persistence;

public sealed class SheetYarDbContextFactory : IDesignTimeDbContextFactory<SheetYarDbContext>
{
    private const string ConnectionStringEnvironmentVariable = "ConnectionStrings__SheetYar";

    public SheetYarDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Set {ConnectionStringEnvironmentVariable} before running Entity Framework Core commands.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<SheetYarDbContext>();
        optionsBuilder.UseSqlServer(
            connectionString,
            sqlServerOptions =>
                sqlServerOptions.MigrationsAssembly(typeof(SheetYarDbContext).Assembly.FullName));

        return new SheetYarDbContext(optionsBuilder.Options, TimeProvider.System);
    }
}
