using Common.Messaging.Contracts;

namespace Common.Messaging.Commands.WithdrawMoney;

public class WithdrawMoneyFromSavingAccount : WithdrawMoneyBase {
    public WithdrawMoneyFromSavingAccount(Guid accountNumber, double amount) 
        : base(accountNumber, amount) {
    }

    public override Task<WithdrawMoneyResult> Accept(IWithdrawMoneyCommandVisitor visitor) {
        return visitor.Visit(this);
    }
}