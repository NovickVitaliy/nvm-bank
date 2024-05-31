using Checkings.API.Models.Dtos;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Get;

public record GetCheckingAccountQuery(Guid AccountId) : IQuery<GetCheckingAccountResult>;

public record GetCheckingAccountResult(Result<CheckingAccountDto> Result);

public class GetCheckingAccountHandler : IQueryHandler<GetCheckingAccountQuery, GetCheckingAccountResult>
{
    public Task<GetCheckingAccountResult> Handle(GetCheckingAccountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}