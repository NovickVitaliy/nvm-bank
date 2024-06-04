namespace Common.Accounts.CheckingAccount;

public static class CheckingAccountConstants
{
    public static readonly int MaximumNumberOfCheckingAccounts = 5;
    public static readonly TimeSpan DaysAccountClosedBeforeDeleted = TimeSpan.FromDays(14);
}