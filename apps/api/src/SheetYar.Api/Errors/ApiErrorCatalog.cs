namespace SheetYar.Api.Errors;

public sealed record ApiErrorDescriptor(
    int Status,
    string Code,
    string Title,
    string Detail);

public static class ApiErrorCatalog
{
    public static IReadOnlyList<int> SupportedStatusCodes { get; } =
    [
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound,
        StatusCodes.Status409Conflict,
        StatusCodes.Status413PayloadTooLarge,
        StatusCodes.Status422UnprocessableEntity,
        StatusCodes.Status429TooManyRequests,
        StatusCodes.Status500InternalServerError,
    ];

    public static ApiErrorDescriptor Get(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => new(statusCode, "bad_request", "Bad Request", "The request could not be processed."),
        StatusCodes.Status401Unauthorized => new(statusCode, "unauthorized", "Unauthorized", "Authentication is required to access this resource."),
        StatusCodes.Status403Forbidden => new(statusCode, "forbidden", "Forbidden", "You do not have permission to access this resource."),
        StatusCodes.Status404NotFound => new(statusCode, "not_found", "Not Found", "The requested resource was not found."),
        StatusCodes.Status409Conflict => new(statusCode, "conflict", "Conflict", "The request conflicts with the current state of the resource."),
        StatusCodes.Status413PayloadTooLarge => new(statusCode, "payload_too_large", "Payload Too Large", "The request payload exceeds the allowed size."),
        StatusCodes.Status422UnprocessableEntity => new(statusCode, "validation_failed", "Validation Failed", "One or more validation errors occurred."),
        StatusCodes.Status429TooManyRequests => new(statusCode, "rate_limit_exceeded", "Too Many Requests", "Too many requests were submitted. Try again later."),
        StatusCodes.Status500InternalServerError => new(statusCode, "internal_server_error", "Internal Server Error", "An unexpected error occurred."),
        _ => new(statusCode, "http_error", "Request Failed", "The request could not be completed."),
    };
}
