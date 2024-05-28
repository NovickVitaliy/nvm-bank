using System.Reflection;
using Common.Logging;
using Common.Messaging.Extension;
using Notification.API.Services.Emailing;
using Notification.API.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();
builder.Services.ConfigureMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection(EmailSettings.Position))
    .ValidateOnStart();

var app = builder.Build();


app.Run();