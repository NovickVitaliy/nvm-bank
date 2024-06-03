using Checkings.API.Data.Repository;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Checkings.API.CheckingAccount.Commands.Reopen;

public record ReopenCheckingAccountCommand(Guid AccountId) : ICommand<ReopenCheckingAccountResult>;

public record ReopenCheckingAccountResult(Result<CheckingAccountReopenedDto> Result);

public class ReopenCheckingAccountHandler : ICommandHandler<ReopenCheckingAccountCommand, ReopenCheckingAccountResult>
{
    private readonly ICheckingsRepository _checkingsRepository;

    public ReopenCheckingAccountHandler(ICheckingsRepository checkingsRepository)
    {
        _checkingsRepository = checkingsRepository;
    }

    public async Task<ReopenCheckingAccountResult> Handle(ReopenCheckingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.ReopenAccount(request.AccountId);

        if (result.IsSuccess)
        {
            //TODO: Sent CheckingAccountReopened to notify user using email
        }

        return new ReopenCheckingAccountResult(result);
    }
}