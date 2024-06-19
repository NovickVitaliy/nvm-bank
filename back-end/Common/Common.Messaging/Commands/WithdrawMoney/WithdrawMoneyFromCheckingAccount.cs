using Common.Messaging.Contracts;

namespace Common.Messaging.Commands.WithdrawMoney;

public class WithdrawMoneyFromCheckingAccount : WithdrawMoneyBase {
    public WithdrawMoneyFromCheckingAccount(Guid accountNumber, double amount)
        : base(accountNumber, amount) {
    }

    public override Task<WithdrawMoneyResult> Accept(IWithdrawMoneyCommandVisitor visitor) {
        return visitor.Visit(this);
    }
}