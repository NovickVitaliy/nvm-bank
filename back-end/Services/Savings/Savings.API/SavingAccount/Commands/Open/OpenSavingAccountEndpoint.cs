using Carter;
using Common.Accounts.SavingAccount;
using Common.ApiResponses;
using Common.Extensions;
using Mapster;
using MediatR;
using Savings.API.Models.Domain;

namespace Savings.API.SavingAccount.Commands.Open;

public record OpenSavingAccountRequest(
    string OwnerEmail, 
    string Currency,
    AccountType AccountType,
    InterestAccrualPeriod AccrualPeriod);

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
                var cmd = request.Adapt<OpenSavingAccountCommand>();

                var result = await sender.Send(cmd);

                return result.Result.ToHttpResponse<OpenedSavingAccountDto, OpenSavingAccountResponse>();
            }).RequireAuthorization();
    }
}