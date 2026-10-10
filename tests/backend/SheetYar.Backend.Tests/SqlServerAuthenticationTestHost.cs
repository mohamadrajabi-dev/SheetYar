using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SheetYar.Api;
using SheetYar.Infrastructure.Persistence;

namespace SheetYar.Backend.Tests;

internal sealed class SqlServerAuthenticationTestHost : IAsyncDisposable
{
    private readonly WebApplication application;

    private SqlServerAuthenticationTestHost(WebApplication application, HttpClient client)
    {
        this.application = application;
        Client = client;
    }

    public HttpClient Client { get; }

    public static async Task<SqlServerAuthenticationTestHost> StartAsync(
        string serverConnectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(serverConnectionString)
        {
            InitialCatalog = $"SheetYarAuthTests_{Guid.NewGuid():N}",
        };
        var connectionString = connectionStringBuilder.ConnectionString;
        var application = ApiApplication.Build(
            Array.Empty<string>(),
            "Testing",
            builder => builder.Configuration.AddTestAuthentication(
                new Dictionary<string, string?>
                {
                    ["ConnectionStrings:SheetYar"] = connectionString,
                }));

        try
        {
            await using (var scope = application.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SheetYarDbContext>();
                await dbContext.Database.MigrateAsync();
            }

            application.Urls.Clear();
            application.Urls.Add("http://127.0.0.1:0");
            await application.StartAsync();
            var server = application.Services.GetRequiredService<IServer>();
            var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
            var address = Xunit.Assert.Single(addresses ?? Array.Empty<string>());
            return new SqlServerAuthenticationTestHost(
                application,
                new HttpClient { BaseAddress = new Uri(address) });
        }
        catch
        {
            try
            {
                await DeleteDatabaseAsync(application);
            }
            catch
            {
                // Preserve the startup failure; a test database can be removed manually if needed.
            }

            try
            {
                await application.DisposeAsync();
            }
            catch
            {
                // Preserve the startup failure.
            }

            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        var failures = new List<Exception>();
        await CaptureFailureAsync(() => application.StopAsync(), failures);
        await CaptureFailureAsync(() => DeleteDatabaseAsync(application), failures);
        await CaptureFailureAsync(() => application.DisposeAsync().AsTask(), failures);

        if (failures.Count > 0)
        {
            throw new AggregateException("SQL Server test host cleanup failed.", failures);
        }
    }

    private static async Task DeleteDatabaseAsync(WebApplication application)
    {
        await using var scope = application.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SheetYarDbContext>();
        await dbContext.Database.EnsureDeletedAsync();
    }

    private static async Task CaptureFailureAsync(
        Func<Task> operation,
        ICollection<Exception> failures)
    {
        try
        {
            await operation();
        }
        catch (Exception exception)
        {
            failures.Add(exception);
        }
    }
}
