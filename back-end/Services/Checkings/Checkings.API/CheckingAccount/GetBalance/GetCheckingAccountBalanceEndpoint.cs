using Carter;
using Checkings.API.Models.Dtos;
using MediatR;

namespace Checkings.API.CheckingAccount.GetBalance;

public record GetCheckingAccountBalanceResponse(AccountBalanceDto AccountBalance);

public class GetCheckingAccountBalanceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}/balance", 
            async (Guid accountId, ISender sender) =>
            {
                var query = new GetCheckingAccountBalanceQuery(accountId);

                var result = await sender.Send(query);
            });
    }
}