using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SheetYar.Application.Health;
using SheetYar.Infrastructure.Configuration;
using SheetYar.Infrastructure.Persistence;

namespace SheetYar.Infrastructure.Health;

public sealed class DatabaseHealthService(
    ISheetYarConnectionStringProvider connectionStringProvider,
    IServiceScopeFactory scopeFactory,
    ILogger<DatabaseHealthService> logger) : IDatabaseHealthService
{
    public async ValueTask<bool> CanConnectAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!connectionStringProvider.TryGet(out _))
        {
            logger.LogWarning("Database health check failed because the connection string is not configured.");
            return false;
        }

        try
        {
            await using var scope = scopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SheetYarDbContext>();
            return await dbContext.Database.CanConnectAsync(cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Database health check failed.");
            return false;
        }
    }
}
