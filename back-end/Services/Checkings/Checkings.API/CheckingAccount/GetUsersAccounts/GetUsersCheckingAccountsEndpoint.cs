using Carter;
using Checkings.API.Models.Dtos;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.GetUsersAccounts;


public record GetUsersCheckingAccountsResponse : BaseHttpResponse<IReadOnlyCollection<CheckingAccountDto>>
{
    public GetUsersCheckingAccountsResponse(IReadOnlyCollection<CheckingAccountDto> value) : base(value)
    {
    }

    public GetUsersCheckingAccountsResponse() : this(value:default!)
    {
        
    }
}

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

                return result.Result
                    .ToHttpResponse<IReadOnlyCollection<CheckingAccountDto>, GetUsersCheckingAccountsResponse>();
            })
            .RequireAuthorization();
    }
}