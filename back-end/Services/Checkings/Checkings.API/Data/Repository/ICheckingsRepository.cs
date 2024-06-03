using Checkings.API.CheckingAccount.Commands.Open;
using Checkings.API.Models.Dtos;
using Common.ErrorHandling;

namespace Checkings.API.Data.Repository;

public interface ICheckingsRepository
{
    Task<Result<CheckingAccountOpenedDto>> OpenAccount(string ownerEmail, string currency);
    Task<(Result<bool> Result, string Email, Guid AccountNumber)> CloseAccount(Guid id, bool isAware);
    Task<Result<CheckingAccountDto>> GetAccount(Guid id);
    Task<Result<AccountBalanceDto>> GetBalance(Guid id);
    Task<Result<IReadOnlyCollection<CheckingAccountDto>>> GetUsersAccounts(string ownerEmail);
}