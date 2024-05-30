using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.API.Models.Domain;

namespace Users.API.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasQueryFilter(x => !x.IsDeleted);
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired();

        builder.HasAlternateKey(x => x.Email);
        
        builder
            .HasMany(x => x.PhoneNumbers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.ComplexProperty(x => x.Address)
            .IsRequired();
    }
}