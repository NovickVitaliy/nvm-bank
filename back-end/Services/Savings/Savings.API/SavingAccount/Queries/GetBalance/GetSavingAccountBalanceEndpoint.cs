using Carter;
using Common.Accounts.Common;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;

namespace Savings.API.SavingAccount.Queries.GetBalance;

public record GetSavingAccountBalanceResponse : BaseHttpResponse<AccountBalanceDto>
{
    public GetSavingAccountBalanceResponse(AccountBalanceDto value) : base(value)
    { }

    public GetSavingAccountBalanceResponse() : this(value:default!)
    { }
}

public class GetSavingAccountBalanceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/savings/accounts/{accountId:guid}/balance", 
            async (Guid accountId, ISender sender) =>
            {
                var query = new GetSavingAccountBalanceQuery(accountId);

                var result = await sender.Send(query);

                return result.Result.ToHttpResponse<AccountBalanceDto, GetSavingAccountBalanceResponse>();
            }).RequireAuthorization();
    }
}