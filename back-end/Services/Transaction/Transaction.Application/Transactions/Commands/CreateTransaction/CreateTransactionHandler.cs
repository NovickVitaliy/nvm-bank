using Common.CQRS.Handlers;
using Transaction.Application.Data;

namespace Transaction.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : ICommandHandler<CreateTransactionCommand, CreateTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<CreateTransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.CreateTransaction(request.CreateTransactionDto);

        if (result.IsSuccess)
        {
            //TODO: send events to appropriate apis
        }

        return new CreateTransactionResult(result);
    }
}