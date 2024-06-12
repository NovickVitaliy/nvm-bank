using Common.CQRS.Handlers;
using Common.ErrorHandling;
using Common.Messaging.Contracts;
using Common.Messaging.Events.CheckAccountMoney;
using MassTransit;
using Transaction.Application.Contracts;
using Transaction.Application.Data;
using Transaction.Application.Helpers;

namespace Transaction.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : ICommandHandler<CreateTransactionCommand, CreateTransactionResult> {
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountMoneyChecker _accountMoneyChecker;
    private readonly IAccountExistenceChecker _accountExistenceChecker;

    public CreateTransactionHandler(ITransactionRepository transactionRepository,
        IAccountMoneyChecker accountMoneyChecker, IAccountExistenceChecker accountExistenceChecker) {
        _transactionRepository = transactionRepository;
        _accountMoneyChecker = accountMoneyChecker;
        _accountExistenceChecker = accountExistenceChecker;
    }

    public async Task<CreateTransactionResult> Handle(CreateTransactionCommand cmd, CancellationToken cancellationToken) {
        var (success, resultOfEnoughMoney) = await CheckIfEnoughMoneyOnSource(cmd);

        if (!success) {
            return new CreateTransactionResult(resultOfEnoughMoney!);
        }

        (success, var resultOfDestinationExistence) = await CheckIfDestinationExists(cmd);

        if (!success) {
            return new CreateTransactionResult(resultOfDestinationExistence!);
        }

        var result = await _transactionRepository.CreateTransaction(cmd.CreateTransactionDto);

        if (result.IsSuccess) {
            //TODO: send events to appropriate apis
        }

        return new CreateTransactionResult(result);
    }

    private async Task<(bool success, Result<Guid>? resultOfDestinationExistence)> CheckIfDestinationExists(
        CreateTransactionCommand cmd) {
        var request = CheckAcccountExistanceRequestFactory.GetRequest(cmd.CreateTransactionDto.DestinationType,
            cmd.CreateTransactionDto.Destination);

        var checkAccountExistanceResult = await request.Accept(_accountExistenceChecker);

        return checkAccountExistanceResult is { Exists: false }
            ? (false, Result<Guid>.Failure(Error.NotFound(request.GetType().Name, request.AccountNumber.ToString())))
            : (true, null);
    }

    private async Task<(bool Success, Result<Guid>? result)> CheckIfEnoughMoneyOnSource(CreateTransactionCommand cmd) {
        var request =
            CheckAccountMoneyRequestFactory.GetRequest(cmd.CreateTransactionDto.SourceType,
                cmd.CreateTransactionDto.Source, cmd.CreateTransactionDto.Amount);

        var checkAccountMoneyResult = await request.Accept(_accountMoneyChecker);

        return checkAccountMoneyResult is { IsEnoughMoney: false, ErrorMessage: not null }
            ? (false, Result<Guid>.Failure(Error.BadRequest(checkAccountMoneyResult.ErrorMessage)))
            : (true, null);
    }
}