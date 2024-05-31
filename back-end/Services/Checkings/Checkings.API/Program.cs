using Checkings.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CheckingsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CheckingsDb"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();