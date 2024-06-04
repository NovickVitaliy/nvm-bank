using Carter;
using Common.ApiResponses;
using MediatR;

namespace Savings.API.SavingAccount.Commands.Open;

public record OpenSavingAccountRequest(string OwnerEmail, string Currency);

public record OpenedSavingAccountDto(Guid Id, Guid AccountNumber);

public record OpenSavingAccountResponse : BaseHttpResponse<OpenedSavingAccountDto>
{
    public OpenSavingAccountResponse(OpenedSavingAccountDto value) : base(value)
    {
    }

    public OpenSavingAccountResponse() : this(value: default)
    {
    }
}

public class OpenSavingAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/savings/accounts", 
            async (OpenSavingAccountRequest request, ISender sender) =>
        {
            //TODO: create cmd -> send and get result -> return response
        }).RequireAuthorization();
    }
}