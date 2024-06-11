using System.Reflection;
using Common.CQRS.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Transaction.Application;

public static class ApplicationExtensions {
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services) {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(assembly);

            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}