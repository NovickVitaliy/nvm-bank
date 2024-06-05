using System.Reflection;
using System.Text.Json.Serialization;
using Carter;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.ErrorHandling;
using Common.Logging;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Savings.API.Data;
using Savings.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

builder.ConfigureLogging();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.RegisterServicesFromAssembly(assembly);
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.ConfigureMessageBroker(builder.Configuration, assembly);

builder.Services.Configure<JsonOptions>(config =>
{
    config.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCarter(configurator: config =>
{
    config.WithValidatorLifetime(ServiceLifetime.Scoped);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddDbContext<SavingDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString(SavingDbContext.ConnectionStringName));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.Run();