using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;
using Transaction.Application.Dto;

namespace Transaction.Application.Transactions.Queries.GetTransaction;

public record GetTransactionQuery(Guid Id) : IQuery<GetTransactionResult>;

public record GetTransactionResult(Result<TransactionDto> Result);

public class GetTransactionQueryValidator : AbstractValidator<GetTransactionQuery> {
    public GetTransactionQueryValidator() {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Transaction Id cannot be empty.");
    }
}