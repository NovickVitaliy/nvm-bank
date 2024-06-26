using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.API.Models.Domain;

namespace Users.API.Data.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("PhoneNumbers");

        builder.HasQueryFilter(x => !x.IsDeleted);
        
        builder.Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.HasAlternateKey(x => x.Number);

        builder.HasOne(x => x.User)
            .WithMany(x => x.PhoneNumbers)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}