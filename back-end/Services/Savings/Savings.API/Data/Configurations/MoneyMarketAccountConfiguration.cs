using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Savings.API.Models.Domain;

namespace Savings.API.Data.Configurations;

public class MoneyMarketAccountConfiguration : IEntityTypeConfiguration<MoneyMarketAccount>
{
    public void Configure(EntityTypeBuilder<MoneyMarketAccount> builder)
    {
        builder.ToTable("MoneyMarketAccounts");

        builder.Property(x => x.WithdrawalLimitsPerMonth)
            .IsRequired();
    }
}