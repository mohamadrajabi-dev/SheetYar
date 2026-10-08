namespace SheetYar.Api.Errors;

public sealed record ApiProblemResponse(
    string Type,
    string Title,
    int Status,
    string Detail,
    string Instance,
    string Code,
    string CorrelationId,
    IReadOnlyDictionary<string, string[]> Errors);
