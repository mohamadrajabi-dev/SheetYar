using Microsoft.Extensions.Configuration;

namespace SheetYar.Infrastructure.Configuration;

internal sealed class SheetYarConnectionStringProvider(IConfiguration configuration)
    : ISheetYarConnectionStringProvider
{
    private const string ConnectionStringName = "SheetYar";

    public bool TryGet(out string connectionString)
    {
        connectionString = configuration.GetConnectionString(ConnectionStringName) ?? string.Empty;
        return !string.IsNullOrWhiteSpace(connectionString);
    }

    public string GetRequired()
    {
        if (TryGet(out var connectionString))
        {
            return connectionString;
        }

        throw new InvalidOperationException(
            "The ConnectionStrings:SheetYar configuration value is required for database access.");
    }
}
