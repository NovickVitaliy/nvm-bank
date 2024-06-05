using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Savings.API.Data.Configurations;

public class SavingAccountConfiguration : IEntityTypeConfiguration<Models.Domain.SavingAccount>
{
    public void Configure(EntityTypeBuilder<Models.Domain.SavingAccount> builder)
    {
        builder.UseTpcMappingStrategy();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Balance)
            .IsRequired();

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(x => x.AccrualPeriod)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.AccountNumber)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.EmailOwner)
            .IsRequired();

        builder.Property(x => x.InterestRate)
            .IsRequired();

        builder.HasAlternateKey(x => x.AccountNumber);
    }
}