using Common.Accounts.Common.Status;
using Common.Accounts.SavingAccount;

namespace Savings.API.Models.Domain;

public abstract class SavingAccount
{
    public Guid Id { get; set; }
    public required string EmailOwner { get; set; }
    public required Guid AccountNumber { get; set; }
    public required double Balance { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required AccountStatus Status { get; set; }
    public required string Currency { get; set; }
    public DateTime? ClosedOn { get; set; }
    public required double InterestRate { get; set; }
    public required InterestAccrualPeriod AccrualPeriod {get; set; }
}