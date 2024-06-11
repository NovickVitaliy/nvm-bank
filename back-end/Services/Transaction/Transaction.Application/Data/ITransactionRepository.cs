using Common.ErrorHandling;
using Transaction.Application.Dto;

namespace Transaction.Application.Data;

public interface ITransactionRepository
{
    Task<Result<Guid>> CreateTransaction(CreateTransactionDto createTransactionDto);
    Task<Result<TransactionDto>> GetTransaction(Guid id);
    Task<Result<IReadOnlyList<TransactionDto>>> GetTransactionsByAccount(Guid accountNumber);
}