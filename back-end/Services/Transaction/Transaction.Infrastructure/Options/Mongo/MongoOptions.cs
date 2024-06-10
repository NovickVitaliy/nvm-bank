namespace Transaction.Infrastructure.Options.Mongo;

public record MongoOptions(
    string ConnectionString, 
    string DatabaseName, 
    string CollectionName);