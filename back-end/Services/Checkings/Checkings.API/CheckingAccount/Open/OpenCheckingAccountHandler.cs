using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Open;

public record OpenCheckingAccountCommand(string OwnerEmail, string Currency) : ICommand<OpenCheckingAccountResult>;

public record OpenCheckingAccountResult(Result<(string Id, string AccountNumber)> Result);

public class OpenCheckingAccountHandler : ICommandHandler<OpenCheckingAccountCommand, OpenCheckingAccountResult>
{
    public Task<OpenCheckingAccountResult> Handle(OpenCheckingAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}