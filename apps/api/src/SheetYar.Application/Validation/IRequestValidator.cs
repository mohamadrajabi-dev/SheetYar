namespace SheetYar.Application.Validation;

public interface IRequestValidator<in TRequest>
    where TRequest : class
{
    IReadOnlyDictionary<string, string[]> Validate(TRequest request);
}
