using Carter;
using Common.ApiResponses;
using Common.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Close;

public record CloseCheckingAccountRequest(Guid AccountId, bool IsAware = false);

public record CloseCheckingAccountResponse : BaseHttpResponse<bool>
{
    public CloseCheckingAccountResponse(bool value) : base(value)
    {
    }

    public CloseCheckingAccountResponse() : this(value: default)
    {
    }
}

public class CloseCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/checkings/accounts/{accountId}",
                async (
                    [FromBody] CloseCheckingAccountRequest req,
                    [FromServices] ISender sender) =>
                {
                    var cmd = req.Adapt<CloseCheckingAccountCommand>();

                    var result = await sender.Send(cmd);

                    return result.Result.ToHttpResponse<bool, CloseCheckingAccountResponse>();
                })
            .RequireAuthorization();
    }
}