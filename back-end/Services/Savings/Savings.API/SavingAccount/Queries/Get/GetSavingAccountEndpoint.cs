using Carter;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Queries.Get;

public record GetSavingAccountResponse : BaseHttpResponse<SavingAccountDto>
{
    public GetSavingAccountResponse(SavingAccountDto value) : base(value)
    { }

    public GetSavingAccountResponse() : this(value:default)
    {
        
    }
}

public class GetSavingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/savings/accounts/{accountId:guid}", 
            async (Guid accountId, ISender sender) =>
            {
                var query = new GetSavingAccountQuery(accountId);

                var result = await sender.Send(query);

                return result.Result.ToHttpResponse<SavingAccountDto, GetSavingAccountResponse>();
            })
            .RequireAuthorization();
    }
}