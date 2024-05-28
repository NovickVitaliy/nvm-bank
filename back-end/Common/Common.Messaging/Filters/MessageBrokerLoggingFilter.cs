using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.Messaging.Filters;

public class MessageBrokerLoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly ILogger<MessageBrokerLoggingFilter<T>> _logger;

    public MessageBrokerLoggingFilter(ILogger<MessageBrokerLoggingFilter<T>> logger)
    {
        _logger = logger;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        _logger.LogInformation("Accepted event '{Name}': {@Event}", context.Message.GetType().Name, context.Message);

        await next.Send(context);
        
        _logger.LogInformation("Handled event '{Name}'", context.Message.GetType().Name);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateScope("message-broker-logging-filter");
    }
}