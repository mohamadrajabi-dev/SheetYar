using SheetYar.Api;
using SheetYar.Infrastructure.Persistence;

var app = ApiApplication.Build(args);

if (app.Configuration.GetValue<bool>("Database:InitializeOnStartup"))
{
    await app.Services.MigrateAndSeedSheetYarDatabaseAsync(app.Lifetime.ApplicationStopping);
}

await app.RunAsync();

public partial class Program
{
}
