using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Savings.API.Data.Repository;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Queries.Get;

public record GetSavingAccountQuery(Guid AccountId) : IQuery<GetSavingAccountResult>;

public record GetSavingAccountResult(Result<SavingAccountDto> Result);

public class GetSavingAccountHandler : IQueryHandler<GetSavingAccountQuery, GetSavingAccountResult>
{
    private readonly ISavingsRepository _savingsRepository;

    public GetSavingAccountHandler(ISavingsRepository savingsRepository)
    {
        _savingsRepository = savingsRepository;
    }

    public async Task<GetSavingAccountResult> Handle(GetSavingAccountQuery request, CancellationToken cancellationToken)
    {
        var result = await _savingsRepository.GetAccount(request.AccountId);

        return new GetSavingAccountResult(result);
    }
}