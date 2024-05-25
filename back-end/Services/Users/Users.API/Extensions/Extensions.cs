using Microsoft.EntityFrameworkCore;
using Users.API.Data;

namespace Users.API.Extensions;

public static class Extensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}