using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Checkings.API.CheckingAccount.GetUsersAccounts;


public record GetUsersCheckingAccountsResponse(IReadOnlyCollection<CheckingAccountDto> CheckingAccounts);

public class GetUsersCheckingAccountsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/users/{userEmail}/accounts", 
            async (
                string userEmail, 
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var query = new GetUsersCheckingAccountsQuery(userEmail);

                var result = await sender.Send(query);

                if (result.Result.IsFailure)
                {
                    result.Result.Error.WriteToResponse(httpContextAccessor.HttpContext!.Response);
                    return Results.StatusCode(result.Result.Error.Code);
                }

                return Results.Ok(new GetUsersCheckingAccountsResponse(result.Result.Value));
            });
    }
}