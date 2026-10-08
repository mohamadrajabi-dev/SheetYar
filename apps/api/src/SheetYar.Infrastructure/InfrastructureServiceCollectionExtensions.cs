using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SheetYar.Application.Health;
using SheetYar.Application.Time;
using SheetYar.Infrastructure.Configuration;
using SheetYar.Infrastructure.Health;
using SheetYar.Infrastructure.Persistence;
using SheetYar.Infrastructure.Time;

namespace SheetYar.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddSheetYarInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<TimeProvider>(TimeProvider.System);
        services.AddSingleton<ISystemClock, SystemClock>();
        services.AddSingleton<ISheetYarConnectionStringProvider>(
            new SheetYarConnectionStringProvider(configuration));

        services.AddDbContext<SheetYarDbContext>((serviceProvider, options) =>
        {
            var connectionString = serviceProvider
                .GetRequiredService<ISheetYarConnectionStringProvider>()
                .GetRequired();

            options.UseSqlServer(
                connectionString,
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly(typeof(SheetYarDbContext).Assembly.FullName);
                    sqlServerOptions.EnableRetryOnFailure();
                });
        });

        services.AddScoped<DatabaseSeeder>();
        services.AddScoped<IDatabaseHealthService, DatabaseHealthService>();
        services.AddScoped<IHealthService, HealthService>();

        return services;
    }
}
