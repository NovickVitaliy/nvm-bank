using Checkings.API.Data.Repository;
using Checkings.API.Models.Dtos;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Get;

public record GetCheckingAccountQuery(Guid AccountId) : IQuery<GetCheckingAccountResult>;

public record GetCheckingAccountResult(Result<CheckingAccountDto> Result);

public class GetCheckingAccountHandler : IQueryHandler<GetCheckingAccountQuery, GetCheckingAccountResult>
{
    private readonly ICheckingsRepository _checkingsRepository;

    public GetCheckingAccountHandler(ICheckingsRepository checkingsRepository)
    {
        _checkingsRepository = checkingsRepository;
    }

    public async Task<GetCheckingAccountResult> Handle(GetCheckingAccountQuery request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.GetAccount(request.AccountId);

        return new GetCheckingAccountResult(result);
    }
}