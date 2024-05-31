using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Close;

public record CloseCheckingAccountCommand(Guid AccountId, bool IsAware) : ICommand<CloseCheckingAccountResult>;

public record CloseCheckingAccountResult(Result<bool> Result);

public class CloseCheckingAccountHandler : ICommandHandler<CloseCheckingAccountCommand, CloseCheckingAccountResult>
{
    public Task<CloseCheckingAccountResult> Handle(CloseCheckingAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}