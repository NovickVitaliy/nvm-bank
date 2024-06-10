using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Transaction.Infrastructure.Options.Mongo;

public class MongoOptionsConfigurator : IConfigureOptions<MongoOptions>
{
    private readonly IConfiguration _configuration;
    private const string Position = "MongoDBSettings";
    
    public MongoOptionsConfigurator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(MongoOptions options)
    {
        _configuration
            .GetSection(Position)
            .Bind(options);
    }
}