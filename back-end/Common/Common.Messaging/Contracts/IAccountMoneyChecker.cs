using Common.Messaging.Events.CheckAccountMoney;

namespace Common.Messaging.Contracts;

public interface IAccountMoneyChecker {
    Task<CheckAccountMoneyResult> Visit(CheckCheckingAccountMoney checkCheckingAccountMoney);
    Task<CheckAccountMoneyResult> Visit(CheckSavingsAccountMoney checkSavingsAccountMoney);
}