using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SheetYar.Api.Errors;
using SheetYar.Domain.Users;
using SheetYar.Infrastructure.Authentication;

namespace SheetYar.Api.Authentication;

internal static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddSheetYarAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtOptions>>((options, jwtOptionsAccessor) =>
            {
                var jwtOptions = jwtOptionsAccessor.Value;
                options.MapInboundClaims = false;
                options.SaveToken = false;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.GetSigningKeyBytes()),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    NameClaimType = JwtRegisteredClaimNames.Name,
                    RoleClaimType = ClaimTypes.Role,
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ValidateUserAsync,
                };
            });
        services.AddAuthorization();

        services.AddOptions<AuthRateLimitOptions>()
            .Bind(configuration.GetSection(AuthRateLimitOptions.SectionName))
            .Validate(AreRateLimitsValid, "Authentication rate limits must use positive permit limits and windows.")
            .ValidateOnStart();
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = WriteRateLimitProblemAsync;
            options.AddPolicy(
                AuthRateLimitPolicies.Register,
                context => CreatePartition(
                    context,
                    AuthRateLimitPolicies.Register,
                    static value => value.RegisterPermitLimit,
                    static value => value.RegisterWindowMinutes));
            options.AddPolicy(
                AuthRateLimitPolicies.Login,
                context => CreatePartition(
                    context,
                    AuthRateLimitPolicies.Login,
                    static value => value.LoginPermitLimit,
                    static value => value.LoginWindowMinutes));
            options.AddPolicy(
                AuthRateLimitPolicies.Refresh,
                context => CreatePartition(
                    context,
                    AuthRateLimitPolicies.Refresh,
                    static value => value.RefreshPermitLimit,
                    static value => value.RefreshWindowMinutes));
            options.AddPolicy(
                AuthRateLimitPolicies.Authenticated,
                context => CreatePartition(
                    context,
                    AuthRateLimitPolicies.Authenticated,
                    static value => value.AuthenticatedPermitLimit,
                    static value => value.AuthenticatedWindowMinutes));
        });

        return services;
    }

    private static async Task ValidateUserAsync(TokenValidatedContext context)
    {
        var userIdValue = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var securityStamp = context.Principal?.FindFirstValue("security_stamp");
        if (!Guid.TryParse(userIdValue, out var userId) || string.IsNullOrWhiteSpace(securityStamp))
        {
            context.Fail("The access token is invalid.");
            return;
        }

        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
        var user = await userManager.FindByIdAsync(userId.ToString("D"));
        if (user is null ||
            !user.IsActive ||
            !string.Equals(user.SecurityStamp, securityStamp, StringComparison.Ordinal))
        {
            context.Fail("The access token is no longer valid.");
        }
    }

    private static RateLimitPartition<string> CreatePartition(
        HttpContext context,
        string policyName,
        Func<AuthRateLimitOptions, int> permitLimit,
        Func<AuthRateLimitOptions, int> windowMinutes)
    {
        var configured = context.RequestServices
            .GetRequiredService<IOptions<AuthRateLimitOptions>>()
            .Value;
        var remoteAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            $"{policyName}:{remoteAddress}",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = permitLimit(configured),
                Window = TimeSpan.FromMinutes(windowMinutes(configured)),
                QueueLimit = 0,
                AutoReplenishment = true,
            });
    }

    private static async ValueTask WriteRateLimitProblemAsync(
        OnRejectedContext context,
        CancellationToken cancellationToken)
    {
        var httpContext = context.HttpContext;
        httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            httpContext.Response.Headers.RetryAfter = Math.Max(1, (int)Math.Ceiling(retryAfter.TotalSeconds))
                .ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        var problemDetails = ApiProblemDetailsFactory.Create(
            httpContext,
            StatusCodes.Status429TooManyRequests);
        await httpContext.RequestServices
            .GetRequiredService<IProblemDetailsService>()
            .WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
            });
    }

    private static bool AreRateLimitsValid(AuthRateLimitOptions options) =>
        options.RegisterPermitLimit > 0 &&
        options.RegisterWindowMinutes > 0 &&
        options.LoginPermitLimit > 0 &&
        options.LoginWindowMinutes > 0 &&
        options.RefreshPermitLimit > 0 &&
        options.RefreshWindowMinutes > 0 &&
        options.AuthenticatedPermitLimit > 0 &&
        options.AuthenticatedWindowMinutes > 0;
}
