using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Open;

public record OpenCheckingAccountRequest(string OwnerEmail, string Currency);

public record OpenCheckingsAccountResponse(string Id, string AccountNumber);

public class OpenCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkings/accounts", 
            async (
                [FromBody] OpenCheckingAccountRequest req, 
                [FromServices] ISender sender) =>
            {
                var cmd = req.Adapt<OpenCheckingAccountCommand>();

                var result = await sender.Send(cmd);
                
                if (result.Result.IsFailure)
                {
                    return Results.BadRequest(result.Result.Error);
                }

                return Results.Ok(new OpenCheckingsAccountResponse(result.Result.Value.Id, result.Result.Value.AccountNumber));
            })
            .RequireAuthorization();
    }
}