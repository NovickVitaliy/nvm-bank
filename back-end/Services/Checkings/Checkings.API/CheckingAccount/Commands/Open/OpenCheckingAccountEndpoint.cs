using Carter;
using Common.ApiResponses;
using Common.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Commands.Open;

public record OpenCheckingAccountRequest(string OwnerEmail, string Currency);

public record CheckingAccountOpenedDto(string Id, string AccountNumber);
public record OpenCheckingsAccountResponse : BaseHttpResponse<CheckingAccountOpenedDto>
{
    public OpenCheckingsAccountResponse(CheckingAccountOpenedDto value) : base(value)
    { }

    public OpenCheckingsAccountResponse() : this(value:default!)
    {
    }
}

public class OpenCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkings/accounts", 
            async (
                [FromBody] OpenCheckingAccountRequest req, 
                [FromServices] ISender sender) =>
            {
                var cmd = req.Adapt<OpenCheckingAccountCommand>();

                var result = await sender.Send(cmd);

                return result.Result.ToHttpResponse<CheckingAccountOpenedDto, OpenCheckingsAccountResponse>();
            })
            .RequireAuthorization();
    }
}