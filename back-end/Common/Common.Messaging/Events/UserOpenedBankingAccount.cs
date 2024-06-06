namespace Common.Messaging.Events;

public class UserOpenedBankingAccount
{
    public required string Email { get; init; }
    public required string AccountType { get; init; }
    public required Guid AccountNumber { get; init; }
}