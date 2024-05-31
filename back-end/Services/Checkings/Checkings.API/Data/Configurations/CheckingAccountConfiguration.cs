using Common.Accounts.Status;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkings.API.Data.Configurations;

public class CheckingAccountConfiguration : IEntityTypeConfiguration<Models.Domain.CheckingAccount>
{
    public void Configure(EntityTypeBuilder<Models.Domain.CheckingAccount> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => x.Status != AccountStatus.Closed);
        
        builder.HasAlternateKey(x => x.OwnerEmail);

        builder.Property(x => x.Balance)
            .IsRequired();

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.AccountNumber)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}