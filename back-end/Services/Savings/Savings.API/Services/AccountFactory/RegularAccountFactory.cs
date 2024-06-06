using Common.Accounts.Common.Status;
using Common.Accounts.SavingAccount;
using Savings.API.Models.Domain;
using Savings.API.Models.Dtos;

namespace Savings.API.Services.AccountFactory;

public class RegularAccountFactory : IAccountFactory
{
    public AccountType AccountType { get; } = AccountType.Regular;

    public Models.Domain.SavingAccount CreateAccount(CreateAccountDto createAccountDto)
    {
        return new RegularAccount
        {
            EmailOwner = createAccountDto.OwnerEmail,
            AccountNumber = Guid.NewGuid(),
            Balance = 0,
            CreatedAt = DateTime.UtcNow,
            Status = AccountStatus.Active,
            Currency = createAccountDto.Currency,
            InterestRate = SavingAccountConstants.RegularInterestRateInPercent,
            AccrualPeriod = createAccountDto.AccrualPeriod
        };
    }
}