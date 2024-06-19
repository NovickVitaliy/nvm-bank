using Common.Messaging.Contracts;

namespace Common.Messaging.Events.CheckAccountMoney;

public abstract class CheckAccountMoneyBase {
    public Guid AccountNumber { get; init; }
    public double TransferAmount { get; init; }
    protected CheckAccountMoneyBase(Guid accountNumber, double transferAmount) {
        AccountNumber = accountNumber;
        TransferAmount = transferAmount;
    }

    public abstract Task<CheckAccountMoneyResult> Accept(IAccountMoneyChecker accountMoneyChecker);
}