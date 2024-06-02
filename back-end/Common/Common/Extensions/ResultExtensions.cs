using Common.ApiResponses;
using Common.ErrorHandling;
using Microsoft.AspNetCore.Http;

namespace Common.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<TResultValue, THttpResponse>(
        this Result<TResultValue> result)
        where THttpResponse : BaseHttpResponse<TResultValue>, new() => result.Error.Code switch
    {
        200 => Results.Ok(new THttpResponse()
        {
            Value = result.Value
        }),
        400 => Results.BadRequest(result.Error),
        401 => Results.Unauthorized(),
        404 => Results.BadRequest(result.Error),
        409 => Results.Conflict(result.Error),
        422 => Results.UnprocessableEntity(result.Error),
        _ => Results.StatusCode(500)
    };
}