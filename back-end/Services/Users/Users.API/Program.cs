using System.Reflection;
using Carter;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.ErrorHandling;
using Common.Logging;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Users.API.Data;
using Users.API.Data.Repository;
using Users.API.Extensions;
using Users.API.MappingConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();

builder.Services.AddCarter();

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<UsersDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("UsersDb"));
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();

MapsterConfiguration.ConfigureMappings();

builder.Services.ConfigureMessageBroker(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.UseExceptionHandler(options => {});

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.Run();