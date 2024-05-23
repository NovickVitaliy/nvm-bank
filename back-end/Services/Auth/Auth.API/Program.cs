using System.Reflection;
using System.Text;
using Auth.API.Data;
using Auth.API.Extensions;
using Auth.API.Services.Auth;
using Auth.API.Services.Token;
using Auth.API.Settings;
using Carter;
using Common.CQRS.Behaviours;
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

builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("JwtSettings:Secret")!)),
            ValidAudience = config.GetValue<string>("JwtSettings:Audience"),
            ValidIssuer = config.GetValue<string>("JwtSettings:Issuer"),
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.SetupDatabase();
}

app.MapCarter();

app.Run();