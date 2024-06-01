using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Carter;
using Checkings.API.Data;
using Checkings.API.Data.Repository;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.ErrorHandling;
using Common.Logging;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
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

builder.Services.ConfigureMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

builder.Services.Configure<JsonOptions>(config =>
{
    config.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCarter(configurator: config =>
{
    config.WithValidatorLifetime(ServiceLifetime.Scoped);
});

builder.Services.AddDbContext<CheckingsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CheckingsDb"));
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<ICheckingsRepository, CheckingsRepository>();

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.Run();