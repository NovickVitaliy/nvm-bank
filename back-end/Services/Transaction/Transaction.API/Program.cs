using System.Reflection;
using Common.Auth;
using Common.CQRS.Behaviours;
using Common.ErrorHandling;
using Common.Logging;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Transaction.Application;
using Transaction.Infrastructure;

[assembly:ApiController]

var assembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.ConfigureLogging();

builder.Services.ConfigureApplicationLayer();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.ConfigureInfrastructureLayer(builder.Configuration);

builder.Services.ConfigureMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();