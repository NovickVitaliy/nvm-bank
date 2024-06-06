using Common.Accounts.SavingAccount;

namespace Savings.API.Models.Dtos;

public record CreateAccountDto(
    string OwnerEmail,
    string Currency,
    InterestAccrualPeriod AccrualPeriod);