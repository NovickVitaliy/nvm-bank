using Checkings.API.Data.Repository;
using Checkings.API.Models.Dtos;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.GetUsersAccounts;

public record GetUsersCheckingAccountsQuery(string UserEmail) : IQuery<GetUsersCheckingAccountsResult>;

public record GetUsersCheckingAccountsResult(Result<IReadOnlyCollection<CheckingAccountDto>> Result);

public class GetUsersCheckingAccountHandler : IQueryHandler<GetUsersCheckingAccountsQuery, GetUsersCheckingAccountsResult>
{
    private readonly ICheckingsRepository _checkingsRepository;

    public GetUsersCheckingAccountHandler(ICheckingsRepository checkingsRepository)
    {
        _checkingsRepository = checkingsRepository;
    }

    public async Task<GetUsersCheckingAccountsResult> Handle(GetUsersCheckingAccountsQuery request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.GetUsersAccounts(request.UserEmail);

        return new GetUsersCheckingAccountsResult(result);
    }
}