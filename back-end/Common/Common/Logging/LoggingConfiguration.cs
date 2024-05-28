using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Common.Logging;

public static class LoggingConfiguration
{
    public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        return builder;
    }
}