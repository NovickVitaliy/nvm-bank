using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Checkings.API.CheckingAccount.Open;

public record OpenCheckingAccountRequest(string OwnerEmail, string Currency);

public record OpenCheckingsAccountResponse(string Id, string AccountNumber);

public class OpenCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkings/accounts", 
            async (
                OpenCheckingAccountRequest req, 
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var cmd = req.Adapt<OpenCheckingAccountCommand>();

                var result = await sender.Send(cmd);
                
                if (result.Result.IsFailure)
                {
                    result.Result.Error.WriteToResponse(httpContextAccessor.HttpContext!.Response);
                    return Results.StatusCode(result.Result.Error.Code);
                }

                return Results.Ok(new OpenCheckingsAccountResponse(result.Result.Value.Id, result.Result.Value.AccountNumber));
            });
    }
}