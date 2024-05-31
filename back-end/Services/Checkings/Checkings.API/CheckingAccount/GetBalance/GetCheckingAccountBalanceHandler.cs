using Checkings.API.Models.Dtos;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.GetBalance;

public record GetCheckingAccountBalanceQuery(Guid AccountId) : IQuery<GetCheckingAccountBalanceResult>;

public record GetCheckingAccountBalanceResult(Result<AccountBalanceDto> Result);

public class GetCheckingAccountBalanceHandler : IQueryHandler<GetCheckingAccountBalanceQuery, GetCheckingAccountBalanceResult>
{
    public Task<GetCheckingAccountBalanceResult> Handle(GetCheckingAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}