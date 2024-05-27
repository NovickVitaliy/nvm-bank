using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Extension;

public static class Extensions
{
    public static void ConfigureMessageBroker(
        this IServiceCollection services, 
        IConfiguration configuration, 
        Assembly? assembly = null)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();

            if (assembly is not null)
            {
                configurator.AddConsumers(assembly);
            }
            
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host(new Uri(configuration["MessageBroker:Host"!]), hostConfigurator =>
                {
                    hostConfigurator.Username(configuration["MessageBroker:Username"]);
                    hostConfigurator.Password(configuration["MessageBroker:Password"]);
                });
                
                factoryConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}