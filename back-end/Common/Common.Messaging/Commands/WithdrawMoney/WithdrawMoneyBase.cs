using Common.Messaging.Contracts;

namespace Common.Messaging.Commands.WithdrawMoney;

public abstract class WithdrawMoneyBase {
    public Guid AccountNumber { get; init; }
    public double Amount { get; init; }
    protected WithdrawMoneyBase(Guid accountNumber, double amount) {
        AccountNumber = accountNumber;
        Amount = amount;
    }

    public abstract Task<WithdrawMoneyResult> Accept(IWithdrawMoneyCommandVisitor visitor);
}