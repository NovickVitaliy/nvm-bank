using Common.Messaging.Events.CheckAccountMoney;

namespace Transaction.Application.Helpers;

public static class CheckAccountMoneyRequestFactory {
    public static CheckAccountMoneyBase GetRequest(string type, Guid accountNumber, double transferAmount)
        => type switch {
            "savings" => new CheckSavingsAccountMoney(accountNumber, transferAmount),
            "checkings" => new CheckCheckingAccountMoney(accountNumber, transferAmount),
            _ => throw new InvalidOperationException("Invalid account type")
        };
}