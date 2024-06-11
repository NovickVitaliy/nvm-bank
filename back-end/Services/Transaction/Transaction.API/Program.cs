using System.Reflection;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.Logging;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Transaction.Infrastructure;

[assembly:ApiController]

var assembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {

        var builtInFactory = options.InvalidModelStateResponseFactory;

        options.InvalidModelStateResponseFactory = context => {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

            logger.LogError("Invalid model state at route {Route}", context.HttpContext.Request.Path.ToString());

            return builtInFactory(context);
        };
    });

builder.ConfigureLogging();

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(assembly);

    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Services.ConfigureMessageBroker(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();