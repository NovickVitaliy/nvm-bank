using Carter;
using Checkings.API.Models.Dtos;
using MediatR;

namespace Checkings.API.CheckingAccount.Get;

public record GetCheckingsAccountResponse(CheckingAccountDto CheckingAccount);

public class GetCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}", 
            async (Guid accountId, ISender sender) =>
            {
                var query = new GetCheckingAccountQuery(accountId);

                var result = await sender.Send(query);
            });
    }
}