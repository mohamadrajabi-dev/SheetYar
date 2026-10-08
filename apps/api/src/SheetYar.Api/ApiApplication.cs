using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using SheetYar.Api.Errors;
using SheetYar.Api.Health;
using SheetYar.Api.Middleware;
using SheetYar.Api.Validation;
using SheetYar.Application.Validation;
using SheetYar.Infrastructure;

namespace SheetYar.Api;

public static class ApiApplication
{
    public static WebApplication Build(
        string[] args,
        string? environmentName = null,
        Action<WebApplicationBuilder>? configureBuilder = null)
    {
        var builder = environmentName is null
            ? WebApplication.CreateBuilder(args)
            : WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ApplicationName = typeof(ApiApplication).Assembly.FullName,
                EnvironmentName = environmentName,
            });

        ConfigureLogging(builder.Logging);
        builder.WebHost.ConfigureKestrel(options =>
            options.Limits.MaxRequestBodySize = RequestSizeLimitMiddleware.MaxRequestBodySize);

        ConfigureServices(builder.Services, builder.Configuration);
        configureBuilder?.Invoke(builder);

        var app = builder.Build();
        ConfigurePipeline(app);
        app.MapHealthEndpoint();

        return app;
    }

    private static void ConfigureLogging(ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.AddJsonConsole(options =>
        {
            options.IncludeScopes = true;
            options.UseUtcTimestamp = true;
            options.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        });
    }

    private static void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSheetYarInfrastructure(configuration);
        services.AddSingleton(typeof(IRequestValidator<>), typeof(DataAnnotationsRequestValidator<>));

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
                ApiProblemDetailsFactory.Enrich(context.HttpContext, context.ProblemDetails);
        });
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddAuthorization();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SheetYar API",
                Version = "v1",
                Description = "The REST API for the SheetYar mobile application.",
            });
        });

        var culture = CultureInfo.GetCultureInfo("en-US");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture);
            options.SupportedCultures = [culture];
            options.SupportedUICultures = [culture];
        });
    }

    private static void ConfigurePipeline(WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseExceptionHandler();
        app.UseStatusCodePages(async statusCodeContext =>
        {
            var httpContext = statusCodeContext.HttpContext;
            var problemDetails = ApiProblemDetailsFactory.Create(
                httpContext,
                httpContext.Response.StatusCode);

            await httpContext.RequestServices
                .GetRequiredService<IProblemDetailsService>()
                .WriteAsync(new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    ProblemDetails = problemDetails,
                });
        });
        app.UseRequestLocalization();
        app.UseMiddleware<RequestSizeLimitMiddleware>();
        app.UseAuthorization();
        app.UseSwagger(options => options.RouteTemplate = "openapi/{documentName}.json");
    }
}
