using SheetYar.Api.Errors;
using SheetYar.Application.Validation;

namespace SheetYar.Api.Validation;

public sealed class ValidationFilter<T> : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<T>().FirstOrDefault();
        if (request is null)
        {
            return await next(context);
        }

        var validator = context.HttpContext.RequestServices.GetRequiredService<IRequestValidator<T>>();
        var errors = validator.Validate(request);
        if (errors.Count == 0)
        {
            return await next(context);
        }

        return new ApiProblemResult(ApiProblemDetailsFactory.Create(
            context.HttpContext,
            StatusCodes.Status422UnprocessableEntity,
            errors));
    }
}
