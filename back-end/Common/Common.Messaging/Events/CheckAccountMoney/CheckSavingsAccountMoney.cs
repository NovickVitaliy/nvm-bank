using Transaction.Application.Contracts;

namespace Common.Messaging.Events.CheckAccountMoney;

public class CheckSavingsAccountMoney : CheckAccountMoneyBase {
    public CheckSavingsAccountMoney(Guid accountNumber, double transferAmount) : base(accountNumber, transferAmount) {
    }

    public override async Task<CheckAccountMoneyResult> Accept(IAccountMoneyChecker accountMoneyChecker) {
        return await accountMoneyChecker.Visit(this);
    }
}