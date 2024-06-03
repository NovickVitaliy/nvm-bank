using Carter;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;

namespace Checkings.API.CheckingAccount.Commands.Reopen;

public record CheckingAccountReopenedDto(bool IsSuccess);

public record ReopenCheckingAccountResponse : BaseHttpResponse<CheckingAccountReopenedDto>
{
    public ReopenCheckingAccountResponse(CheckingAccountReopenedDto value) : base(value)
    {
    }

    public ReopenCheckingAccountResponse() : this(value: default!)
    {
    }
}

public class ReopenCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkings/accounts/{accountId:guid}/reopen",
            async (Guid accountId, ISender sender) =>
            {
                var cmd = new ReopenCheckingAccountCommand(accountId);

                var result = await sender.Send(cmd);

                return result.Result.ToHttpResponse<CheckingAccountReopenedDto, ReopenCheckingAccountResponse>();
            });
    }
}