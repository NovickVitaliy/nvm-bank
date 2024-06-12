using Common.Messaging.Events.CheckAccountMoney;

namespace Transaction.Application.Contracts;

public interface IAccountMoneyChecker {
    Task<CheckAccountMoneyResult> Visit(CheckCheckingAccountMoney checkCheckingAccountMoney);
    Task<CheckAccountMoneyResult> Visit(CheckSavingsAccountMoney checkSavingsAccountMoney);
}