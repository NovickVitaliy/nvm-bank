using Common.Messaging.Commands.WithdrawMoney;

namespace Transaction.Application.Helpers;

public static class WithdrawMoneyFromAccountCommandFactory {
    public static WithdrawMoneyBase GetWithdrawMoneyCommand(string type, Guid accountNumber, double amount)
        => type switch {
            "savings" => new WithdrawMoneyFromSavingAccount(accountNumber, amount),
            "checkings" => new WithdrawMoneyFromCheckingAccount(accountNumber, amount),
            _ => throw new InvalidOperationException("Invalid account type")
        };
}