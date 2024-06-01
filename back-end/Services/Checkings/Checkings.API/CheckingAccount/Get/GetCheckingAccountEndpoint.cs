using Carter;
using Checkings.API.Models.Dtos;
using Common.ErrorHandling;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Get;

public record GetCheckingsAccountResponse(CheckingAccountDto CheckingAccount);

public class GetCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}", 
            async (
                [FromRoute] Guid accountId, 
                [FromServices] ISender sender) =>
            {
                var query = new GetCheckingAccountQuery(accountId);

                var result = await sender.Send(query);

                if (result.Result.IsFailure)
                {
                    return Results.BadRequest(result.Result.Error);
                }

                return Results.Ok(new GetCheckingsAccountResponse(result.Result.Value));
            })
            .RequireAuthorization();
    }
}