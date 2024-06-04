using Checkings.API.Data.Repository;
using Checkings.API.Models.Dtos;
using Common.Accounts.Common;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Queries.GetBalance;

public record GetCheckingAccountBalanceQuery(Guid AccountId) : IQuery<GetCheckingAccountBalanceResult>;

public record GetCheckingAccountBalanceResult(Result<AccountBalanceDto> Result);

public class GetCheckingAccountBalanceHandler : IQueryHandler<GetCheckingAccountBalanceQuery, GetCheckingAccountBalanceResult>
{
    private readonly ICheckingsRepository _checkingsRepository;

    public GetCheckingAccountBalanceHandler(ICheckingsRepository checkingsRepository)
    {
        _checkingsRepository = checkingsRepository;
    }

    public async Task<GetCheckingAccountBalanceResult> Handle(GetCheckingAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.GetBalance(request.AccountId);

        return new GetCheckingAccountBalanceResult(result);
    }
}