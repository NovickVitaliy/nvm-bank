using Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.ErrorHandling;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is BaseException baseException)
        {
            var apiResponse = baseException.ToApiResponse(httpContext.Request.Path);

            await apiResponse.WriteToResponseAsJsonAsync(httpContext.Response);
        }
        else
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Error Occured",
                Instance = httpContext.Request.Path,
                Detail = exception.Message,
                Type = exception.GetType().Name,
                Status = 500
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        }
        return true;
    }
}