using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Savings.API.SavingAccount.Commands.Open;

public record OpenSavingAccountCommand(string OwnerEmail, string Currency) : ICommand<OpenSavingAccountResult>;

public record OpenSavingAccountResult(Result<OpenedSavingAccountDto> Result);

public class OpenSavingAccountHandler : ICommandHandler<OpenSavingAccountCommand, OpenSavingAccountResult>
{
    public Task<OpenSavingAccountResult> Handle(OpenSavingAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}