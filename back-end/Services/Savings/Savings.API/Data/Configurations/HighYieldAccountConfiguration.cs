using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Savings.API.Models.Domain;

namespace Savings.API.Data.Configurations;

public class HighYieldAccountConfiguration : IEntityTypeConfiguration<HighYieldAccount>
{
    public void Configure(EntityTypeBuilder<HighYieldAccount> builder)
    {
        builder.ToTable("HighYieldAccounts");

        builder.Property(x => x.WithdrawalLimitsPerMonth)
            .IsRequired();
    }
}