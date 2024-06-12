using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Transaction.Application.Contracts;
using Transaction.Application.Data;
using Transaction.Infrastructure.Options.Mongo;
using Transaction.Infrastructure.Repositories;
using Transaction.Infrastructure.Services;

namespace Transaction.Infrastructure;

public static class InfrastractureExtensions
{
    public static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<MongoOptionsConfigurator>();
        
        services.AddSingleton<IMongoClient>((sp) =>
        {
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>().Value;

            return new MongoClient(mongoOptions.ConnectionString);
        });

        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IAccountMoneyChecker, DefaultAccountMoneyChecker>();
        
        return services;
    }
}