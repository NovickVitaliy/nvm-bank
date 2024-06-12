namespace Common.Messaging.Events.CheckAccountMoney;

public class CheckAccountMoneyResult {
    public bool IsEnoughMoney { get; init; }
    public string? ErrorMessage { get; init; }
    
    public CheckAccountMoneyResult(bool isEnoughMoney, string errorMessage = "") {
        IsEnoughMoney = isEnoughMoney;
        ErrorMessage = errorMessage;
    }
}