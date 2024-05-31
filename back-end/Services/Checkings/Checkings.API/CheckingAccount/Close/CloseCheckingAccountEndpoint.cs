using Carter;
using Mapster;
using MediatR;

namespace Checkings.API.CheckingAccount.Close;

public record CloseCheckingAccountRequest(Guid AccountId, bool IsAwareOfConsequences = false);

public record CloseCheckingAccountResponse(bool IsSuccess);

public class CloseCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/checkings/accounts/{accountId}",
            async (CloseCheckingAccountRequest req, ISender sender) =>
            {
                var cmd = req.Adapt<CloseCheckingAccountCommand>();

                var result = await sender.Send(cmd);
            });
    }
}