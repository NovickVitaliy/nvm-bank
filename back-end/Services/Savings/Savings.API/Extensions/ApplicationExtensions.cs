using Microsoft.EntityFrameworkCore;
using Savings.API.Data;

namespace Savings.API.Extensions;

public static class ApplicationExtensions
{
    public static async Task<IApplicationBuilder> MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<SavingDbContext>();

        await dbContext.Database.MigrateAsync();
        
        return app;
    }
}