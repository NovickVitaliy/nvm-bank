using Common.Accounts.SavingAccount;
using Savings.API.Models.Domain;

namespace Savings.API.Models.Dtos;

public record CreateAccountDto(
    string OwnerEmail,
    string Currency,
    InterestAccrualPeriod AccrualPeriod,
    AccountType AccountType);