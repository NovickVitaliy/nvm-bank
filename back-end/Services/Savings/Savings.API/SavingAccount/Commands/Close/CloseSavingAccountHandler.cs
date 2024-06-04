using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Savings.API.SavingAccount.Commands.Close;

public record CloseSavingAccountCommand(Guid AccountId, bool IsAware) : ICommand<CloseSavingAccountResult>;

public record CloseSavingAccountResult(Result<bool> Result);

public class CloseSavingAccountHandler : ICommandHandler<CloseSavingAccountCommand, CloseSavingAccountResult>
{
    public Task<CloseSavingAccountResult> Handle(CloseSavingAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}