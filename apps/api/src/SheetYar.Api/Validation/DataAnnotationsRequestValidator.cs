using System.ComponentModel.DataAnnotations;
using SheetYar.Application.Validation;

namespace SheetYar.Api.Validation;

public sealed class DataAnnotationsRequestValidator<T> : IRequestValidator<T>
    where T : class
{
    public IReadOnlyDictionary<string, string[]> Validate(T request)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        Validator.TryValidateObject(
            request,
            validationContext,
            validationResults,
            validateAllProperties: true);

        return validationResults
            .SelectMany(result =>
            {
                var memberNames = result.MemberNames.Any()
                    ? result.MemberNames
                    : ["request"];

                return memberNames.Select(memberName => new
                {
                    MemberName = memberName,
                    Message = result.ErrorMessage ?? "The value is invalid.",
                });
            })
            .GroupBy(error => error.MemberName, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Message).Distinct().ToArray(),
                StringComparer.OrdinalIgnoreCase);
    }
}
