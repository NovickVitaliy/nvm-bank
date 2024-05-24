using System.Reflection;
using Carter;
using Common.CQRS.Behaviours;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();