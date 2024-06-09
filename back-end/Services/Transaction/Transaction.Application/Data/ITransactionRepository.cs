using Transaction.Application.Dto;

namespace Transaction.Application.Data;

public interface ITransactionRepository
{
    Task<Guid> CreateTransaction(CreateTransactionDto createTransactionDto);
    Task<TransactionDto> GetTransaction(Guid id);
    Task<IEnumerable<TransactionDto>> GetTransactionsByAccount(Guid accountNumber);
}