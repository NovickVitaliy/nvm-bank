using Common.Accounts.Common.Status;
using Common.Accounts.SavingAccount;
using Savings.API.Models.Domain;
using Savings.API.Models.Dtos;

namespace Savings.API.Services.AccountFactory;

public class MoneyMarketAccountFactory : IAccountFactory
{
    public AccountType AccountType { get; } = AccountType.MoneyMarket;

    public Models.Domain.SavingAccount CreateAccount(CreateAccountDto createAccountDto)
    {
        return new MoneyMarketAccount
        {
            WithdrawalLimitsPerMonth = SavingAccountConstants.MoneyMarketAccountWithdrawalLimitPerMonth,
            EmailOwner = createAccountDto.OwnerEmail,
            AccountNumber = Guid.NewGuid(),
            Balance = 0,
            CreatedAt = DateTime.UtcNow,
            Status = AccountStatus.Active,
            Currency = createAccountDto.Currency,
            InterestRate = SavingAccountConstants.MoneyMarketInterestRateInPercent,
            AccrualPeriod = createAccountDto.AccrualPeriod
        };
    }
}