using Checkings.API.Data.Repository;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using MassTransit;

namespace Checkings.API.CheckingAccount.Commands.Close;

public record CloseCheckingAccountCommand(Guid AccountId, bool IsAware) : ICommand<CloseCheckingAccountResult>;

public record CloseCheckingAccountResult(Result<bool> Result);

public class CloseCheckingAccountHandler : ICommandHandler<CloseCheckingAccountCommand, CloseCheckingAccountResult>
{
    private readonly ICheckingsRepository _checkingsRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    public CloseCheckingAccountHandler(ICheckingsRepository checkingsRepository, IPublishEndpoint publishEndpoint)
    {
        _checkingsRepository = checkingsRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CloseCheckingAccountResult> Handle(CloseCheckingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.CloseAccount(request.AccountId, request.IsAware);

        if (result.Result.Value)
        {
            await _publishEndpoint.Publish(new UserClosedCheckingAccount()
            {
                Email = result.Email,
                AccountId = request.AccountId
            }, cancellationToken);
        }
        
        return new CloseCheckingAccountResult(result.Result);
    }
}