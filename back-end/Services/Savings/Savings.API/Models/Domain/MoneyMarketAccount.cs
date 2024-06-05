namespace Savings.API.Models.Domain;

public class MoneyMarketAccount : SavingAccount
{
    public required int WithdrawalLimitsPerMonth { get; set; }
}