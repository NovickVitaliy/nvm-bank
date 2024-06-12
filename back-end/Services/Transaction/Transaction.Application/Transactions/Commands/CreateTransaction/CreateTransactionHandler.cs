using Common.CQRS.Handlers;
using Common.ErrorHandling;
using Common.Messaging.Events.CheckAccountMoney;
using MassTransit;
using Transaction.Application.Contracts;
using Transaction.Application.Data;
using Transaction.Application.Helpers;

namespace Transaction.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : ICommandHandler<CreateTransactionCommand, CreateTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountMoneyChecker _accountMoneyChecker;
    public CreateTransactionHandler(ITransactionRepository transactionRepository, IAccountMoneyChecker accountMoneyChecker) {
        _transactionRepository = transactionRepository;
        _accountMoneyChecker = accountMoneyChecker;
    }

    public async Task<CreateTransactionResult> Handle(CreateTransactionCommand cmd, CancellationToken cancellationToken)
    {
        var request =
            CheckAccountMoneyRequestFactory.GetRequest(cmd.CreateTransactionDto.SourceType,
                cmd.CreateTransactionDto.Source, cmd.CreateTransactionDto.Amount);

        var checkAccountMoneyResult = await request.Accept(_accountMoneyChecker);

        if (checkAccountMoneyResult is { IsEnoughMoney: false, ErrorMessage: not null }) {
            return new CreateTransactionResult(Result<Guid>.Failure(Error.BadRequest(checkAccountMoneyResult.ErrorMessage)));
        }
        
        var result = await _transactionRepository.CreateTransaction(cmd.CreateTransactionDto);

        if (result.IsSuccess)
        {
            //TODO: send events to appropriate apis
        }

        return new CreateTransactionResult(result);
    }
}