using System.Reflection;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[assembly:ApiController]

var assembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();