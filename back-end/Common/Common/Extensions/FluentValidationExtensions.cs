using FluentValidation.Results;

namespace Common.Extensions;

public static class FluentValidationExtensions
{
    public static IDictionary<string, string[]> ToValidationErrorsDictionary(
        this IEnumerable<ValidationFailure> failures)
    {
        return failures
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage)
            .ToDictionary(
                x => x.Key, 
                x => x.ToArray());
    }
}