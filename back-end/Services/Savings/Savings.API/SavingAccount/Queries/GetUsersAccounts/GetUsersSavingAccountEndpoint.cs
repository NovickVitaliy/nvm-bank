using Carter;
using Common.ApiResponses;
using MediatR;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Queries.GetUsersAccounts;

public record GetUsersSavingAccountsResponse : BaseHttpResponse<IReadOnlyCollection<SavingAccountDto>>
{
    public GetUsersSavingAccountsResponse(IReadOnlyCollection<SavingAccountDto> value) : base(value)
    {
    }

    public GetUsersSavingAccountsResponse() : this(value:default!)
    {
        
    }
}

public class GetUsersSavingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/savings/users/{userEmail}/accounts", 
            async (string userEmail, ISender sender) =>
        {
            //TODO: create query -> send and get result -> return response
        });
    }
}