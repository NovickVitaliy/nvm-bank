using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Savings.API.Data.Repository;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Queries.GetUsersAccounts;

public record GetUsersSavingAccountsQuery(string UserEmail) : IQuery<GetUsersSavingAccountsResult>;

public record GetUsersSavingAccountsResult(Result<IReadOnlyCollection<SavingAccountDto>> Result);

public class GetUsersSavingAccountsHandler : IQueryHandler<GetUsersSavingAccountsQuery, GetUsersSavingAccountsResult>
{
    private readonly ISavingsRepository _savingsRepository;

    public GetUsersSavingAccountsHandler(ISavingsRepository savingsRepository)
    {
        _savingsRepository = savingsRepository;
    }

    public async Task<GetUsersSavingAccountsResult> Handle(GetUsersSavingAccountsQuery request, CancellationToken cancellationToken)
    {
        var result = await _savingsRepository.GetUsersAccounts(request.UserEmail);

        return new GetUsersSavingAccountsResult(result);
    }
}