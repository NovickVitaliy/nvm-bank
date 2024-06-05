using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Savings.API.Models.Domain;

namespace Savings.API.Data.Configurations;

public class RegularAccountConfiguration : IEntityTypeConfiguration<RegularAccount>
{
    public void Configure(EntityTypeBuilder<RegularAccount> builder)
    {
        builder.ToTable("RegularAccounts");
    }
}