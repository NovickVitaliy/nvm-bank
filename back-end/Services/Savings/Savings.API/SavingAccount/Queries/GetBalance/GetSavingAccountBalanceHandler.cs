using Common.Accounts.Common;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Savings.API.SavingAccount.Queries.GetBalance;

public record GetSavingAccountBalanceQuery(Guid AccountId) : IQuery<GetSavingAccountBalanceResult>;

public record GetSavingAccountBalanceResult(Result<AccountBalanceDto> Result);

public class GetSavingAccountBalanceHandler : IQueryHandler<GetSavingAccountBalanceQuery, GetSavingAccountBalanceResult>
{
    public Task<GetSavingAccountBalanceResult> Handle(GetSavingAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}