using Auth.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Extensions;

public static class Extensions
{
    public static void ConfigurePasswordOptions(this PasswordOptions passwordOptions)
    {
        
    }

    public static async Task SetupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

        await db.Database.MigrateAsync();
    }
}