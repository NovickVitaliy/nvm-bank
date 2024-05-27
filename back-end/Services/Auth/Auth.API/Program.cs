using System.Reflection;
using System.Text;
using Auth.API.BackgroundServices;
using Auth.API.Data;
using Auth.API.Extensions;
using Auth.API.Models.Identity;
using Auth.API.Services.Auth;
using Auth.API.Services.Token;
using Auth.API.Settings;
using Carter;
using Common.CQRS.Behaviours;
using Common.Messaging.Extension;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddOptions<JwtSettings>()
    .Bind(config.GetSection("JwtSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<PasswordSettings>()
    .Bind(config.GetSection(PasswordSettings.Position))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.ConfigurePasswordOptions(builder.Services);
    })
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb"));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddHostedService<DeleteUsersWithUnfinishedRegistrationService>();

builder.Services.ConfigureMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.SetupDatabase();
}

app.MapCarter();

app.Run();