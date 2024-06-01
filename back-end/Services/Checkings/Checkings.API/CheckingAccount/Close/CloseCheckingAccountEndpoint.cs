using Carter;
using Common.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Checkings.API.CheckingAccount.Close;

public record CloseCheckingAccountRequest(Guid AccountId, bool IsAwareOfConsequences = false);

public record CloseCheckingAccountResponse(bool IsSuccess);

public class CloseCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/checkings/accounts/{accountId}",
            async (
                CloseCheckingAccountRequest req, 
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var cmd = req.Adapt<CloseCheckingAccountCommand>();

                var result = await sender.Send(cmd);

                if (result.Result.IsFailure)
                {
                    result.Result.Error.WriteToResponse(httpContextAccessor.HttpContext!.Response);
                    return Results.StatusCode(result.Result.Error.Code);
                }

                return Results.Ok(new CloseCheckingAccountResponse(result.Result.Value));
            });
    }
}