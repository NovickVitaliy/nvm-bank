using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Savings.API.SavingAccount.Commands.Reopen;

public record ReopenSavingAccountCommand(Guid AccountId) : ICommand<ReopenSavingAccountResult>;

public record ReopenSavingAccountResult(Result<bool> Result);

public class ReopenSavingAccountHandler : ICommandHandler<ReopenSavingAccountCommand, ReopenSavingAccountResult>
{
    public Task<ReopenSavingAccountResult> Handle(ReopenSavingAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}