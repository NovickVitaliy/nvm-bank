using Carter;
using Common.ApiResponses;
using Common.Extensions;
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
                var query = new GetUsersSavingAccountsQuery(userEmail);

                var result = await sender.Send(query);

                return result.Result
                    .ToHttpResponse<IReadOnlyCollection<SavingAccountDto>, GetUsersSavingAccountsResponse>();
            })
            .RequireAuthorization();
    }
}