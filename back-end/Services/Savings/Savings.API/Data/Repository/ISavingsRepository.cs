using Common.Accounts.Common;
using Common.ErrorHandling;
using Savings.API.Models.Dtos;
using Savings.API.SavingAccount.Commands.Open;

namespace Savings.API.Data.Repository;

public interface ISavingsRepository
{
    Task<Result<OpenedSavingAccountDto>> OpenAccount(string ownerEmail, string currency);
    Task<(Result<bool> Result, string Email, Guid AccountNumber)> CloseAccount(Guid id, bool isAware);
    Task<Result<SavingAccountDto>> GetAccount(Guid id);
    Task<Result<AccountBalanceDto>> GetBalance(Guid id);
    Task<Result<IReadOnlyCollection<SavingAccountDto>>> GetUsersAccounts(string ownerEmail);
    Task<Result<SavingAccountDto>> ReopenAccount(Guid requestAccountId);
}