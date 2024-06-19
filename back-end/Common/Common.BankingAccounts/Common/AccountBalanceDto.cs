namespace Common.Accounts.Common;

public record AccountBalanceDto(
    Guid AccountId, 
    double Balance);