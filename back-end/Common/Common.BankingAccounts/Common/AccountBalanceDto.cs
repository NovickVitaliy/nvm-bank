namespace Common.Accounts.Common;

public record AccountBalanceDto(
    Guid AccountId, 
    ulong Balance);