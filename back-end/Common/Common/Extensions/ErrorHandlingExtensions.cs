using Common.ErrorHandling;
using Microsoft.AspNetCore.Http;

namespace Common.Extensions;

public static class ErrorHandlingExtensions
{
    public static void WriteToResponse(this Error error, HttpResponse response)
    {
        response.StatusCode = error.Code;
        response.WriteAsJsonAsync(error);
    }
}