using Carter;
using Common.ApiResponses;
using Common.Extensions;
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
                var cmd = new ReopenSavingAccountCommand(accountId);

                var result = await sender.Send(cmd);

                return result.Result.ToHttpResponse<ReopenedSavingAccountDto, ReopenSavingAccountResponse>();
            });
    }
}