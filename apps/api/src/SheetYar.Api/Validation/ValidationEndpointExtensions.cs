namespace SheetYar.Api.Validation;

public static class ValidationEndpointExtensions
{
    public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder)
        where T : class => builder.AddEndpointFilter<ValidationFilter<T>>();
}
