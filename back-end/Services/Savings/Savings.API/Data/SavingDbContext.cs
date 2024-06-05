using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Savings.API.Data;

public class SavingDbContext : DbContext
{
    public static readonly string ConnectionStringName = "SavingsDb";
    public DbSet<Models.Domain.SavingAccount> SavingAccounts => Set<Models.Domain.SavingAccount>();

    public SavingDbContext(DbContextOptions<SavingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}