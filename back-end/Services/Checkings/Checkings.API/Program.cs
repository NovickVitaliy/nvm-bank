using System.Reflection;
using Carter;
using Checkings.API.Data;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.ErrorHandling;
using Common.Logging;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();

builder.Services.ConfigureAuthentication(builder.Configuration);

var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddAuthorization();

builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.RegisterServicesFromAssembly(assembly);
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.ConfigureMessageBroker(builder.Configuration);

builder.Services.AddCarter();

builder.Services.AddDbContext<CheckingsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CheckingsDb"));
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.Run();