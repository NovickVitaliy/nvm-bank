using Common.Accounts.Common.Status;

namespace Checkings.API.Models.Dtos;

public record CheckingAccountDto(
    Guid Id,
    string OwnerEmail,
    Guid AccountNumber,
    ulong Balance,
    DateTime CreatedAt,
    AccountStatus Status,
    string Currency);