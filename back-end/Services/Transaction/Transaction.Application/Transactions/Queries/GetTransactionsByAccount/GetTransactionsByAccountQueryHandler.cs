using Common.CQRS.Handlers;
using Transaction.Application.Data;

namespace Transaction.Application.Transactions.Queries.GetTransactionsByAccount;

public class GetTransactionsByAccountQueryHandler : IQueryHandler<GetTransactionsByAccountQuery, GetTransactionsByAccountResult> {
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByAccountQueryHandler(ITransactionRepository transactionRepository) {
        _transactionRepository = transactionRepository;
    }

    public async Task<GetTransactionsByAccountResult> Handle(GetTransactionsByAccountQuery request, CancellationToken cancellationToken) {
        var result = await _transactionRepository.GetTransactionsByAccount(request.AccountNumber);

        return new GetTransactionsByAccountResult(result);
    }
}