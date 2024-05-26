using Common.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace Common.Exceptions;

public class ValidationException : BaseException
{
    private readonly IDictionary<string, string[]> _validationErrors;

    public ValidationException(IDictionary<string, string[]> validationErrors)
    {
        _validationErrors = validationErrors;
    }

    public override BaseApiResponse ToApiResponse(string errorPath)
    {
        return new ValidationErrorApiResponse
        {
            ValidationErrors = _validationErrors,
            StatusCode = StatusCodes.Status422UnprocessableEntity,
            Type = GetType().Name,
            Title = "Validation Error",
            Path = errorPath
        };
    }
}