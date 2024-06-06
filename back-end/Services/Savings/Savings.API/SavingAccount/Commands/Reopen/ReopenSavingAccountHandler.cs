using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Savings.API.Data.Repository;

namespace Savings.API.SavingAccount.Commands.Reopen;

public record ReopenSavingAccountCommand(Guid AccountId) : ICommand<ReopenSavingAccountResult>;

public record ReopenSavingAccountResult(Result<ReopenedSavingAccountDto> Result);

public class ReopenSavingAccountHandler : ICommandHandler<ReopenSavingAccountCommand, ReopenSavingAccountResult>
{
    private readonly ISavingsRepository _savingsRepository;

    public ReopenSavingAccountHandler(ISavingsRepository savingsRepository)
    {
        _savingsRepository = savingsRepository;
    }

    public async Task<ReopenSavingAccountResult> Handle(ReopenSavingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _savingsRepository.ReopenAccount(request.AccountId);

        if (result.IsSuccess)
        {
            //TODO: send event to notification.api
        }

        return new ReopenSavingAccountResult(result);
    }
}