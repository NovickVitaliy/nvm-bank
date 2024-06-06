using Common.Accounts.Common;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Savings.API.Data.Repository;

namespace Savings.API.SavingAccount.Queries.GetBalance;

public record GetSavingAccountBalanceQuery(Guid AccountId) : IQuery<GetSavingAccountBalanceResult>;

public record GetSavingAccountBalanceResult(Result<AccountBalanceDto> Result);

public class GetSavingAccountBalanceHandler : IQueryHandler<GetSavingAccountBalanceQuery, GetSavingAccountBalanceResult>
{
    private readonly ISavingsRepository _savingsRepository;

    public GetSavingAccountBalanceHandler(ISavingsRepository savingsRepository)
    {
        _savingsRepository = savingsRepository;
    }

    public async Task<GetSavingAccountBalanceResult> Handle(GetSavingAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        var result = await _savingsRepository.GetBalance(request.AccountId);

        return new GetSavingAccountBalanceResult(result);
    }
}