using Carter;
using Checkings.API.Models.Dtos;
using Common.Accounts.Common;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Queries.GetBalance;

public record GetCheckingAccountBalanceResponse : BaseHttpResponse<AccountBalanceDto>
{
    public GetCheckingAccountBalanceResponse(AccountBalanceDto value) : base(value)
    {
    }

    public GetCheckingAccountBalanceResponse() : this(value:default!)
    {
        
    }
}

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

                return result.Result.ToHttpResponse<AccountBalanceDto, GetCheckingAccountBalanceResponse>();
            })
            .RequireAuthorization();
    }
}