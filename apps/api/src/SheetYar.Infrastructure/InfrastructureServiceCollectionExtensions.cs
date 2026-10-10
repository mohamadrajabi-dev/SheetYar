using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SheetYar.Application.Authentication;
using SheetYar.Application.Health;
using SheetYar.Application.Time;
using SheetYar.Domain.Users;
using SheetYar.Infrastructure.Authentication;
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

        services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager()
            .AddEntityFrameworkStores<SheetYarDbContext>();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.Issuer), "JWT issuer is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.Audience), "JWT audience is required.")
            .Validate(options => options.HasValidSigningKey(), "JWT signing key must be valid Base64 containing at least 32 bytes.")
            .Validate(options => options.AccessTokenMinutes is >= 1 and <= 15, "JWT access token lifetime must be between 1 and 15 minutes.")
            .Validate(options => options.RefreshTokenDays is >= 1 and <= 90, "Refresh token lifetime must be between 1 and 90 days.")
            .ValidateOnStart();

        services.AddScoped<IAccessTokenIssuer, AccessTokenIssuer>();
        services.AddScoped<IRefreshTokenFactory, RefreshTokenFactory>();
        services.AddSingleton<LoginTimingProtector>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<DatabaseSeeder>();
        services.AddScoped<IDatabaseHealthService, DatabaseHealthService>();
        services.AddScoped<IHealthService, HealthService>();

        return services;
    }
}
