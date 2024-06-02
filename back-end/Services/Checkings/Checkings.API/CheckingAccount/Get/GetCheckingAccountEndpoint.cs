using Carter;
using Checkings.API.CheckingAccount.Close;
using Checkings.API.Models.Dtos;
using Common.ApiResponses;
using Common.ErrorHandling;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkings.API.CheckingAccount.Get;

public record GetCheckingsAccountResponse : BaseHttpResponse<CheckingAccountDto>
{
    public GetCheckingsAccountResponse(CheckingAccountDto value) : base(value)
    {
    }

    public GetCheckingsAccountResponse() : this(value: default!)
    {
    }
}

public class GetCheckingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/accounts/{accountId:guid}",
                async (
                    [FromRoute] Guid accountId,
                    [FromServices] ISender sender) =>
                {
                    var query = new GetCheckingAccountQuery(accountId);

                    var result = await sender.Send(query);

                    return result.Result.ToHttpResponse<CheckingAccountDto, GetCheckingsAccountResponse>();
                })
            .RequireAuthorization();
    }
}