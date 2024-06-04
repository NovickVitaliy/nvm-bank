using Checkings.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.Extensions;

public static class Extension
{
    public static async Task<IApplicationBuilder> MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<CheckingsDbContext>();

        await dbContext.Database.MigrateAsync();
        
        return app;
    }
}