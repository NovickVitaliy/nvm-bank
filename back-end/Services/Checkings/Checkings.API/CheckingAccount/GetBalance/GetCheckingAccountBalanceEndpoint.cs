using Carter;
using Checkings.API.Models.Dtos;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Checkings.API.CheckingAccount.GetBalance;

public record GetCheckingAccountBalanceResponse(AccountBalanceDto AccountBalance);

public class GetCheckingAccountBalanceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}/balance", 
            async (
                Guid accountId, 
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var query = new GetCheckingAccountBalanceQuery(accountId);

                var result = await sender.Send(query);

                if (result.Result.IsFailure)
                {
                    result.Result.Error.WriteToResponse(httpContextAccessor.HttpContext!.Response);
                    return Results.StatusCode(result.Result.Error.Code);
                }

                return Results.Ok(new GetCheckingAccountBalanceResponse(result.Result.Value));
            });
    }
}