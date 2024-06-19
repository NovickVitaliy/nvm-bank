namespace Common.Messaging.Commands.WithdrawMoney;

public class WithdrawMoneyResult {
    public bool Success { get; init; }

    public WithdrawMoneyResult(bool success) {
        Success = success;
    }
}