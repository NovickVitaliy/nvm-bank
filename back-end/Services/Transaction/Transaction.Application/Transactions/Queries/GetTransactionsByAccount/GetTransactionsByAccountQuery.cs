using Common.CQRS.Requests;
using Common.ErrorHandling;
using Transaction.Application.Dto;

namespace Transaction.Application.Transactions.Queries.GetTransactionsByAccount;

public record GetTransactionsByAccountQuery(Guid AccountNumber) : IQuery<GetTransactionsByAccountResult>;

public record GetTransactionsByAccountResult(Result<IReadOnlyList<TransactionDto>> Result);