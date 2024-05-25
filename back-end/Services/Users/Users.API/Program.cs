using System.Reflection;
using Carter;
using Common.CQRS.Behaviours;
using Microsoft.EntityFrameworkCore;
using Users.API.Data;
using Users.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddDbContext<UsersDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("UsersDb"));
});

builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();