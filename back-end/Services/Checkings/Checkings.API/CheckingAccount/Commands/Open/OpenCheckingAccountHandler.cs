using Checkings.API.Data.Repository;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using Common.Messaging.Events.UserExists;
using FluentValidation;
using MassTransit;

namespace Checkings.API.CheckingAccount.Commands.Open;

public record OpenCheckingAccountCommand(string OwnerEmail, string Currency) : ICommand<OpenCheckingAccountResult>;

public record OpenCheckingAccountResult(Result<CheckingAccountOpenedDto> Result);

public sealed class OpenCheckingsAccountValidtor : AbstractValidator<OpenCheckingAccountCommand>
{
    private readonly IRequestClient<CheckUserExistence> _requestClient;
    public OpenCheckingsAccountValidtor(IRequestClient<CheckUserExistence> requestClient)
    {
        _requestClient = requestClient;
        RuleFor(x => x.OwnerEmail)
            .NotEmpty().WithMessage("Email address of the owner cannot be empty.")
            .EmailAddress().WithMessage("Email address must be in a proper format.")
            .MustAsync(UserWithGivenEmailExists).WithMessage("User with given email does not exist.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency cannot be empty.")
            .Must(x => x.Length == 3).WithMessage("Currency code cannot have more than 3 characters.");
    }

    private async Task<bool> UserWithGivenEmailExists(string email, CancellationToken cancellationToken)
    {
        return (await _requestClient.GetResponse<UserExistenceResponse>(new CheckUserExistence()
        {
            Email = email
        }, cancellationToken)).Message.Exists;
    }
}

public class OpenCheckingAccountHandler : ICommandHandler<OpenCheckingAccountCommand, OpenCheckingAccountResult>
{
    private readonly ICheckingsRepository _checkingsRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    public OpenCheckingAccountHandler(ICheckingsRepository checkingsRepository, IPublishEndpoint publishEndpoint)
    {
        _checkingsRepository = checkingsRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<OpenCheckingAccountResult> Handle(OpenCheckingAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _checkingsRepository.OpenAccount(request.OwnerEmail, request.Currency);

        if (result.IsSuccess)
        {
            await _publishEndpoint.Publish(new UserOpenedBankingAccount
            {
                Email = request.OwnerEmail,
                AccountType = nameof(Models.Domain.CheckingAccount),
                AccountNumber = Guid.Parse(result.Value.AccountNumber),
            }, cancellationToken);
        }
        
        return new OpenCheckingAccountResult(result);
    }
}