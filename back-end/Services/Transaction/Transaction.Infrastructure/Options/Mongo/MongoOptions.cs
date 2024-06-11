namespace Transaction.Infrastructure.Options.Mongo;

public record MongoOptions(
    string ConnectionString,
    string DatabaseName,
    string TransactionsCollectionName) {
    public MongoOptions() : this(default, default, default) {
        
    }
}