using Auth.API.Data;
using Auth.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Auth.API.Extensions;

public static class Extensions
{
    public static void ConfigurePasswordOptions(this PasswordOptions passwordOptions, IServiceCollection services)
    {
        using var sp = services.BuildServiceProvider();
        var passwordSettings = sp.GetRequiredService<IOptions<PasswordSettings>>().Value;
        passwordOptions.RequiredLength = passwordSettings.RequiredLength;
        passwordOptions.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
        passwordOptions.RequireDigit = passwordSettings.RequireDigit;
        passwordOptions.RequireLowercase = passwordSettings.RequireLowercase;
        passwordOptions.RequireUppercase = passwordSettings.RequireUppercase;
    }

    public static async Task SetupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

        await db.Database.MigrateAsync();
    }
}