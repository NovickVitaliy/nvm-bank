using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Queries.Get;

public record GetSavingAccountQuery(Guid AccountId) : IQuery<GetSavingAccountResult>;

public record GetSavingAccountResult(Result<SavingAccountDto> Result);

public class GetSavingAccountHandler : IQueryHandler<GetSavingAccountQuery, GetSavingAccountResult>
{
    public Task<GetSavingAccountResult> Handle(GetSavingAccountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}