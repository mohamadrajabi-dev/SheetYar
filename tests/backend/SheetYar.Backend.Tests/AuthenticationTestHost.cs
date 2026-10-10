using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SheetYar.Api;
using SheetYar.Infrastructure.Persistence;

namespace SheetYar.Backend.Tests;

internal sealed class AuthenticationTestHost : IAsyncDisposable
{
    private readonly WebApplication _application;

    private AuthenticationTestHost(WebApplication application, HttpClient client)
    {
        _application = application;
        Client = client;
    }

    public HttpClient Client { get; }

    public IServiceProvider Services => _application.Services;

    public static async Task<AuthenticationTestHost> StartAsync(
        IReadOnlyDictionary<string, string?>? configurationOverrides = null)
    {
        var databaseName = $"sheetyar-auth-tests-{Guid.NewGuid():N}";
        var application = ApiApplication.Build(
            Array.Empty<string>(),
            "Testing",
            builder =>
            {
                builder.Configuration.AddTestAuthentication(configurationOverrides);
                builder.Services.RemoveAll<SheetYarDbContext>();
                builder.Services.RemoveAll<DbContextOptions<SheetYarDbContext>>();
                var existingDbContextConfigurations = builder.Services
                    .Where(descriptor =>
                        descriptor.ServiceType.IsGenericType &&
                        descriptor.ServiceType.Name.StartsWith(
                            "IDbContextOptionsConfiguration",
                            StringComparison.Ordinal))
                    .ToArray();
                foreach (var descriptor in existingDbContextConfigurations)
                {
                    builder.Services.Remove(descriptor);
                }

                builder.Services.AddDbContext<SheetYarDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName));
            });

        application.Urls.Clear();
        application.Urls.Add("http://127.0.0.1:0");
        await application.StartAsync();
        var server = application.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
        var address = Xunit.Assert.Single(addresses ?? Array.Empty<string>());
        var client = new HttpClient { BaseAddress = new Uri(address) };
        return new AuthenticationTestHost(application, client);
    }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        await _application.DisposeAsync();
    }
}
