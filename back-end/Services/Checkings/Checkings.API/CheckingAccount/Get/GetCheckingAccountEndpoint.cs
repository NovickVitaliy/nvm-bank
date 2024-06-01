using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Checkings.API.CheckingAccount.Get;

public record GetCheckingsAccountResponse(CheckingAccountDto CheckingAccount);

public class GetCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}", 
            async (
                Guid accountId, 
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var query = new GetCheckingAccountQuery(accountId);

                var result = await sender.Send(query);

                if (result.Result.IsFailure)
                {
                    result.Result.Error.WriteToResponse(httpContextAccessor.HttpContext!.Response);
                    return Results.StatusCode(result.Result.Error.Code);
                }

                return Results.Ok(new GetCheckingsAccountResponse(result.Result.Value));
            });
    }
}