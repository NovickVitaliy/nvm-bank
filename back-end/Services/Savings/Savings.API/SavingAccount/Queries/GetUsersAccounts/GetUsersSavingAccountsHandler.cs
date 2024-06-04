using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Queries.GetUsersAccounts;

public record GetUsersSavingAccountsQuery(string UserEmail) : IQuery<GetUsersSavingAccountsResult>;

public record GetUsersSavingAccountsResult(Result<IReadOnlyCollection<SavingAccountDto>> Result);

public class GetUsersSavingAccountsHandler : IQueryHandler<GetUsersSavingAccountsQuery, GetUsersSavingAccountsResult>
{
    public Task<GetUsersSavingAccountsResult> Handle(GetUsersSavingAccountsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}