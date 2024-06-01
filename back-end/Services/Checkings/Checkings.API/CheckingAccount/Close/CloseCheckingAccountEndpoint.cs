using Carter;
using Common.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Close;

public record CloseCheckingAccountRequest(Guid AccountId, bool IsAware = false);

public record CloseCheckingAccountResponse(bool IsSuccess);

public class CloseCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/checkings/accounts/{accountId}",
            async (
                [FromBody] CloseCheckingAccountRequest req, 
                [FromServices] ISender sender) =>
            {
                var cmd = req.Adapt<CloseCheckingAccountCommand>();

                var result = await sender.Send(cmd);

                if (result.Result.IsFailure)
                {
                    return Results.BadRequest(result.Result.Error);
                }

                return Results.Ok(new CloseCheckingAccountResponse(result.Result.Value));
            })
            .RequireAuthorization();
    }
}