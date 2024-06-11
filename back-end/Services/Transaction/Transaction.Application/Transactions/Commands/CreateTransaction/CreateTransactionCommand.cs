using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;
using Transaction.Application.Dto;

namespace Transaction.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(CreateTransactionDto CreateTransactionDto) : ICommand<CreateTransactionResult>;

public record CreateTransactionResult(Result<Guid> Result);

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.CreateTransactionDto.Amount)
            .GreaterThan(0).WithMessage("Amount cannot be less than or equal to zero.");

        RuleFor(x => x.CreateTransactionDto.Currency)
            .NotEmpty().WithMessage("Currency cannot be empty.")
            .Length(3).WithMessage("Currecny code must have only 3 characters.");

        RuleFor(x => x.CreateTransactionDto.DestinationType)
            .NotEmpty().WithMessage("Destination account type cannot be empty.");

        RuleFor(x => x.CreateTransactionDto.Destination)
            .NotEmpty().WithMessage("Destination account number cannot be empty.");

        RuleFor(x => x.CreateTransactionDto.SourceType)
            .NotEmpty().WithMessage("Destination account type cannot be empty.");

        RuleFor(x => x.CreateTransactionDto.Source)
            .NotEmpty().WithMessage("Destination account number cannot be empty.");

        RuleFor(x => x.CreateTransactionDto.DestinationBank)
            .NotEmpty().WithMessage("Destination bank cannot be empty.");
    }
}