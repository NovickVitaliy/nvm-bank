using Common.CQRS.Handlers;
using Transaction.Application.Data;

namespace Transaction.Application.Transactions.Queries.GetTransaction;

public class GetTransactionHandler : IQueryHandler<GetTransactionQuery, GetTransactionResult> {
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionHandler(ITransactionRepository transactionRepository) {
        _transactionRepository = transactionRepository;
    }

    public async Task<GetTransactionResult> Handle(GetTransactionQuery request, CancellationToken cancellationToken) {
        var result = await _transactionRepository.GetTransaction(request.Id);

        return new GetTransactionResult(result);
    }
}