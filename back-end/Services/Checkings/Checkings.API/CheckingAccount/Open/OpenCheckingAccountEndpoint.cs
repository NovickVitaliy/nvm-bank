using Carter;
using Checkings.API.Models.Dtos;
using Mapster;
using MediatR;

namespace Checkings.API.CheckingAccount.Open;

public record OpenCheckingAccountRequest(string OwnerEmail, string Currency);

public record OpenCheckingsAccountResponse(string Id, string AccountNumber);

public class OpenCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkings/accounts", 
            async (OpenCheckingAccountRequest req, ISender sender) =>
            {
                var cmd = req.Adapt<OpenCheckingAccountCommand>();

                var result = await sender.Send(cmd);
            });
    }
}