using Common.Messaging.Commands.WithdrawMoney;

namespace Common.Messaging.Contracts;

public interface IWithdrawMoneyCommandVisitor {
    Task<WithdrawMoneyResult> Visit(WithdrawMoneyFromCheckingAccount command);
    Task<WithdrawMoneyResult> Visit(WithdrawMoneyFromSavingAccount command);
}