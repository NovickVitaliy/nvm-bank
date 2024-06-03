using Checkings.API.Data.Repository;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Commands.Close;

public record CloseCheckingAccountCommand(Guid AccountId, bool IsAware) : ICommand<CloseCheckingAccountResult>;

public record CloseCheckingAccountResult(Result<bool> Result);

public class CloseCheckingAccountHandler : ICommandHandler<CloseCheckingAccountCommand, CloseCheckingAccountResult>
{
    private readonly ICheckingsRepository _checkingsRepository;

    public CloseCheckingAccountHandler(ICheckingsRepository checkingsRepository)
    {
        _checkingsRepository = checkingsRepository;
    }

    public async Task<CloseCheckingAccountResult> Handle(CloseCheckingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.CloseAccount(request.AccountId, request.IsAware);

        return new CloseCheckingAccountResult(result);
    }
}