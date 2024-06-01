using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.GetUsersAccounts;


public record GetUsersCheckingAccountsResponse(IReadOnlyCollection<CheckingAccountDto> CheckingAccounts);

public class GetUsersCheckingAccountsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/users/{userEmail}/accounts", 
            async (
                [FromRoute] string userEmail, 
                [FromServices] ISender sender) =>
            {
                var query = new GetUsersCheckingAccountsQuery(userEmail);

                var result = await sender.Send(query);

                if (result.Result.IsFailure)
                {
                    return Results.BadRequest(result.Result.Error);
                }

                return Results.Ok(new GetUsersCheckingAccountsResponse(result.Result.Value));
            });
    }
}