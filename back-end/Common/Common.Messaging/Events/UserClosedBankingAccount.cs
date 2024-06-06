namespace Common.Messaging.Events;

public class UserClosedBankingAccount
{
    public required Guid AccountNumber { get; init; }
    public required string Email { get; init; }
    public required string AccountType { get; init; } 
}