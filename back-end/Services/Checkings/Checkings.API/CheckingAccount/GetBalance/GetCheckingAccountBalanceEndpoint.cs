using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.GetBalance;

public record GetCheckingAccountBalanceResponse(AccountBalanceDto AccountBalance);

public class GetCheckingAccountBalanceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}/balance", 
            async (
                [FromRoute] Guid accountId, 
                [FromServices] ISender sender) =>
            {
                var query = new GetCheckingAccountBalanceQuery(accountId);

                var result = await sender.Send(query);

                if (result.Result.IsFailure)
                {
                    return Results.BadRequest(result.Result.Error);
                }

                return Results.Ok(new GetCheckingAccountBalanceResponse(result.Result.Value));
            })
            .RequireAuthorization();
    }
}