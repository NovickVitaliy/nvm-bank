using Common.Accounts.SavingAccount;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using Common.Messaging.Events.UserExists;
using FluentValidation;
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

public class OpenSavingAccountCommandValidator : AbstractValidator<OpenSavingAccountCommand>
{
    private readonly IRequestClient<CheckUserExistence> _requestClient;

    public OpenSavingAccountCommandValidator(IRequestClient<CheckUserExistence> requestClient)
    {
        _requestClient = requestClient;
        RuleFor(x => x.OwnerEmail)
            .NotEmpty().WithMessage("Owner's email cannot be empty.")
            .EmailAddress().WithMessage("Owner's email must be in a proper format.")
            .MustAsync(UserWithGivenEmailExists);

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency cannot be empty.")
            .Length(3).WithMessage("Currency length must be only 3 characters.");

        RuleFor(x => x.AccountType)
            .NotEmpty().WithMessage("Account Type cannot be empty.")
            .IsInEnum().WithMessage("Invalid account type.");

        RuleFor(x => x.AccrualPeriod)
            .NotEmpty().WithMessage("Accrual period cannot be empty.")
            .IsInEnum().WithMessage("Invalid accrual period.");
    }
    
    private async Task<bool> UserWithGivenEmailExists(string email, CancellationToken cancellationToken)
    {
        return (await _requestClient.GetResponse<UserExistenceResponse>(new CheckUserExistence()
        {
            Email = email
        }, cancellationToken)).Message.Exists;
    }
}

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