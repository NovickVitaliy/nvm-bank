using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Transaction.Infrastructure.Options.Mongo;

namespace Transaction.Infrastructure;

public static class InfrastractureExtensions
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<MongoOptionsConfigurator>();
        
        services.AddSingleton<IMongoClient>((sp) =>
        {
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>().Value;

            return new MongoClient(mongoOptions.ConnectionString);
        });

        return services;
    }
}