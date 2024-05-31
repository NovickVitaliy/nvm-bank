using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.Data;

public class CheckingsDbContext : DbContext
{
    public DbSet<Models.Domain.CheckingAccount> CheckingAccounts => Set<Models.Domain.CheckingAccount>();
    
    public CheckingsDbContext(DbContextOptions<CheckingsDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}