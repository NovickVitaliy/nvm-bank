namespace Checkings.API.Models.Dtos;

public record AccountBalanceDto(
    Guid AccountId, 
    ulong Balance);