namespace Savings.API.Models.Domain;

public class HighYieldAccount : SavingAccount
{
    public required int WithdrawalLimitsPerMonth { get; set; }
}