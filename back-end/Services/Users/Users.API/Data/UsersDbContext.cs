using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Users.API.Models.Domain;

namespace Users.API.Data;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}