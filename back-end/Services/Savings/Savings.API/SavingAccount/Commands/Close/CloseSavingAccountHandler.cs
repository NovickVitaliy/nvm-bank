using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using MassTransit;
using Savings.API.Data.Repository;

namespace Savings.API.SavingAccount.Commands.Close;

public record CloseSavingAccountCommand(Guid AccountId, bool IsAware) : ICommand<CloseSavingAccountResult>;

public record CloseSavingAccountResult(Result<bool> Result);

public class CloseSavingAccountHandler : ICommandHandler<CloseSavingAccountCommand, CloseSavingAccountResult>
{
    private readonly ISavingsRepository _savingsRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    public CloseSavingAccountHandler(ISavingsRepository savingsRepository, IPublishEndpoint publishEndpoint)
    {
        _savingsRepository = savingsRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CloseSavingAccountResult> Handle(CloseSavingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _savingsRepository.CloseAccount(request.AccountId, request.IsAware);

        if (result.Result.IsSuccess)
        {
            await _publishEndpoint.Publish(new UserClosedBankingAccount()
            {
                Email = result.Email,
                AccountNumber = result.AccountNumber,
                AccountType = nameof(Models.Domain.SavingAccount)
            }, cancellationToken);
        }

        return new CloseSavingAccountResult(result.Result);
    }
}