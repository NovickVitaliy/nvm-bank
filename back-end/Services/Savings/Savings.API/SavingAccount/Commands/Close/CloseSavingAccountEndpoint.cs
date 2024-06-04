using Carter;
using Common.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Savings.API.SavingAccount.Commands.Close;

public record CloseSavingAccountRequest(Guid AccountId, bool IsAware = false);

public record CloseSavingAccountResponse : BaseHttpResponse<bool>
{
    public CloseSavingAccountResponse(bool value) : base(value)
    {
    }

    public CloseSavingAccountResponse() : this(value: default)
    {
    }
}

public class CloseSavingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/savings/accounts/{accountId}",
            async (
                [FromBody] CloseSavingAccountRequest req,
                [FromServices] ISender sender) =>
            {
                //TODO: create cmd -> send and get result -> return response
            }).RequireAuthorization();
    }
}