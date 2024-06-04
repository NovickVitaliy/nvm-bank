using Carter;
using Common.ApiResponses;
using MediatR;

namespace Savings.API.SavingAccount.Commands.Reopen;

public record ReopenedSavingAccountDto(bool IsSuccess);

public record ReopenSavingAccountResponse : BaseHttpResponse<ReopenedSavingAccountDto>
{
    public ReopenSavingAccountResponse(ReopenedSavingAccountDto value) : base(value)
    {
    }

    public ReopenSavingAccountResponse() : this(value: default)
    {
    }
}

public class ReopenSavingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/savings/account/{accountId:guid}/reopen",
            async (Guid accountId, ISender sender) =>
            {
                //TODO: create cmd -> send and get result -> return response
            });
    }
}