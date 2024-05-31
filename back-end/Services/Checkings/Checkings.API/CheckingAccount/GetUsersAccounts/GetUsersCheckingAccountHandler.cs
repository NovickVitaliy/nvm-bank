using Checkings.API.Models.Dtos;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.GetUsersAccounts;

public record GetUsersCheckingAccountsQuery(string UserEmail) : IQuery<GetUsersCheckingAccountsResult>;

public record GetUsersCheckingAccountsResult(Result<IReadOnlyCollection<CheckingAccountDto>> Result);

public class GetUsersCheckingAccountHandler : IQueryHandler<GetUsersCheckingAccountsQuery, GetUsersCheckingAccountsResult>
{
    public Task<GetUsersCheckingAccountsResult> Handle(GetUsersCheckingAccountsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}