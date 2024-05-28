using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.CQRS.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {RequestName}: {@Request}", typeof(TRequest).Name, request);

        var result = await next();

        _logger.LogInformation("Handled {RequestName} with  {RequestResponse}", typeof(TRequest).Name,
            typeof(TResponse).Name);

        return result;
    }
}