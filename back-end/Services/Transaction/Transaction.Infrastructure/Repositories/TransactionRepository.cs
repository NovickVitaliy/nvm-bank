using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Transaction.Application.Data;
using Transaction.Application.Dto;
using Transaction.Application.Extensions;
using Transaction.Domain.ValueObjects;
using Transaction.Infrastructure.Options.Mongo;

namespace Transaction.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly MongoOptions _mongoOptions;
    private readonly IMongoCollection<Domain.Models.Transaction> _transactions;

    public TransactionRepository(
        IMongoClient mongoClient,
        IOptions<MongoOptions> options)
    {
        _mongoClient = mongoClient;
        _mongoOptions = options.Value;
        _transactions = _mongoClient.GetDatabase(_mongoOptions.DatabaseName)
            .GetCollection<Domain.Models.Transaction>(_mongoOptions.TransactionsCollectionName);
    }

    public async Task<Guid> CreateTransaction(CreateTransactionDto createTransactionDto)
    {
        var transaction = Domain.Models.Transaction.Create(
            new AccountNumber(createTransactionDto.Source),
            new AccountType(createTransactionDto.SourceType),
            new AccountNumber(createTransactionDto.Destination),
            new AccountType(createTransactionDto.DestinationType),
            createTransactionDto.Amount,
            createTransactionDto.Currency,
            createTransactionDto.DestinationBank);

        await _transactions.InsertOneAsync(transaction);

        return transaction.Id.Value;
    }

    public async Task<TransactionDto> GetTransaction(Guid id)
    {
        var transaction = await (await _transactions
                .FindAsync(x => x.Id.Equals(new TransactionId(id))))
            .FirstOrDefaultAsync();

        if (transaction is null)
        {
            //TODO: return result response
        }

        return transaction.ToTransactionDto();
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactionsByAccount(Guid accountNumber)
    {
        var transactions = await (await _transactions.FindAsync(x => x.Source.Equals(new AccountNumber(accountNumber))))
            .ToListAsync();

        return transactions.Select(x => x.ToTransactionDto());
    }
}