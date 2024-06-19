using Common.Accounts.Common.Status;

namespace Checkings.API.Models.Domain;

public class CheckingAccount
{
    public Guid Id { get; set; }
    public required string OwnerEmail { get; set; }
    public required Guid AccountNumber { get; set; }
    public required double Balance { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required AccountStatus Status { get; set; }
    public required string Currency { get; set; }
    public DateTime? ClosedOn { get; set; }
}