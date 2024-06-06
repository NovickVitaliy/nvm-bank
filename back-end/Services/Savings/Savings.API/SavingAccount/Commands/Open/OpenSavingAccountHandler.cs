using Common.Accounts.SavingAccount;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using MassTransit;
using Savings.API.Data.Repository;
using Savings.API.Models.Domain;
using Savings.API.Models.Dtos;

namespace Savings.API.SavingAccount.Commands.Open;

public record OpenSavingAccountCommand(
    string OwnerEmail, 
    string Currency,
    AccountType AccountType,
    InterestAccrualPeriod AccrualPeriod) : ICommand<OpenSavingAccountResult>;

public record OpenSavingAccountResult(Result<OpenedSavingAccountDto> Result);

public class OpenSavingAccountHandler : ICommandHandler<OpenSavingAccountCommand, OpenSavingAccountResult>
{
    private readonly ISavingsRepository _savingsRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    public OpenSavingAccountHandler(ISavingsRepository savingsRepository, IPublishEndpoint publishEndpoint)
    {
        _savingsRepository = savingsRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<OpenSavingAccountResult> Handle(OpenSavingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _savingsRepository.OpenAccount(new CreateAccountDto(request.OwnerEmail,
            request.Currency, request.AccrualPeriod, request.AccountType));

        if (result.IsSuccess)
        {
            await _publishEndpoint.Publish(new UserOpenedBankingAccount
            {
                Email = request.OwnerEmail,
                AccountType = nameof(Models.Domain.SavingAccount),
                AccountNumber = result.Value!.AccountNumber
            });
        }
        
        return new OpenSavingAccountResult(result);
    }
}