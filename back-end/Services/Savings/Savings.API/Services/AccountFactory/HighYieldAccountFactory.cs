using Common.Accounts.Common.Status;
using Common.Accounts.SavingAccount;
using Savings.API.Models.Domain;
using Savings.API.Models.Dtos;

namespace Savings.API.Services.AccountFactory;

public class HighYieldAccountFactory : IAccountFactory
{
    public AccountType AccountType { get; } = AccountType.HighYield;

    public Models.Domain.SavingAccount CreateAccount(CreateAccountDto createAccountDto)
    {
        return new HighYieldAccount
        {
            WithdrawalLimitsPerMonth = SavingAccountConstants.HighYieldAccountWithdrawalLimitPerMonth,
            EmailOwner = createAccountDto.OwnerEmail,
            AccountNumber = Guid.NewGuid(),
            Balance = 0,
            CreatedAt = DateTime.UtcNow,
            Status = AccountStatus.Active,
            Currency = createAccountDto.Currency,
            InterestRate = SavingAccountConstants.HighYieldInterestRateInPercent,
            AccrualPeriod = createAccountDto.AccrualPeriod
        };
    }
}