using Common.Accounts.Common;
using Common.Accounts.Common.Status;
using Common.Accounts.SavingAccount;
using Common.ErrorHandling;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Savings.API.Models.Dtos;
using Savings.API.SavingAccount.Commands.Open;
using Savings.API.SavingAccount.Commands.Reopen;
using Savings.API.Services.AccountFactory;

namespace Savings.API.Data.Repository;

public class SavingsRepository : ISavingsRepository
{
    private readonly SavingDbContext _db;
    private readonly IEnumerable<IAccountFactory> _factories;

    public SavingsRepository(SavingDbContext db, IEnumerable<IAccountFactory> factories)
    {
        _db = db;
        _factories = factories;
    }

    public async Task<Result<OpenedSavingAccountDto>> OpenAccount(CreateAccountDto createAccountDto)
    {
        if (await DoesUserHaveMaximumNumberOfAccount(createAccountDto.OwnerEmail,
                SavingAccountConstants.MaximumNumberOfSavingAccounts))
        {
            return Result<OpenedSavingAccountDto>.Failure(Error.BadRequest(
                "You already own 5 checking accounts. Both opened and closed. Please delete the closed account in order to create a new one"));
        }

        var account = _factories.SingleOrDefault(x => x.AccountType == createAccountDto.AccountType)?
            .CreateAccount(createAccountDto);

        if (account is null)
        {
            return Result<OpenedSavingAccountDto>.Failure(Error.BadRequest("Invalid saving account type."));
        }

        await _db.SavingAccounts.AddAsync(account);

        await _db.SaveChangesAsync();

        return Result<OpenedSavingAccountDto>.Success(new OpenedSavingAccountDto(account.Id, account.AccountNumber));
    }

    private async Task<bool> DoesUserHaveMaximumNumberOfAccount(string ownerEmail, int maximumNumberOfSavingAccounts)
    {
        return await _db.SavingAccounts.Where(x => x.EmailOwner == ownerEmail)
            .CountAsync() >= maximumNumberOfSavingAccounts;
    }

    public async Task<(Result<bool> Result, string Email, Guid AccountNumber)> CloseAccount(Guid id, bool isAware)
    {
        if (!isAware)
        {
            return (Result<bool>.Failure(
                    Error.BadRequest("To close the account you must be aware of the possible consequences.")),
                string.Empty, Guid.Empty);
        }

        var savingAccount = await _db.SavingAccounts.FindAsync(id);

        if (savingAccount is null)
        {
            return (Result<bool>.Failure(
                Error.BadRequest("Saving account is not found.")), string.Empty, Guid.Empty);
        }

        if (savingAccount.Status == AccountStatus.Closed)
        {
            return (Result<bool>.Failure(Error.BadRequest("Saving account is already closed")), string.Empty,
                Guid.Empty);
        }

        savingAccount.Status = AccountStatus.Closed;
        savingAccount.ClosedOn = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return (Result<bool>.Success(true), savingAccount.EmailOwner, savingAccount.AccountNumber);
    }

    public async Task<Result<SavingAccountDto>> GetAccount(Guid id)
    {
        var account = await _db.SavingAccounts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (account is null)
        {
            return Result<SavingAccountDto>.Failure(Error.NotFound(nameof(Models.Domain.SavingAccount),
                id.ToString()));
        }

        return Result<SavingAccountDto>.Success(account.Adapt<SavingAccountDto>());
    }

    public async Task<Result<AccountBalanceDto>> GetBalance(Guid id)
    {
        var balance = await _db.SavingAccounts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new AccountBalanceDto(x.Id, x.Balance))
            .SingleOrDefaultAsync();

        if (balance is null)
        {
            return Result<AccountBalanceDto>.Failure(Error.NotFound(nameof(Models.Domain.SavingAccount),
                id.ToString()));
        }

        return Result<AccountBalanceDto>.Success(balance);
    }

    public async Task<Result<IReadOnlyCollection<SavingAccountDto>>> GetUsersAccounts(string ownerEmail)
    {
        var account = await _db.SavingAccounts
            .AsNoTracking()
            .Where(x => x.EmailOwner == ownerEmail)
            .ToListAsync();

        return Result<IReadOnlyCollection<SavingAccountDto>>.Success(
            account
                .Select(x => x.Adapt<SavingAccountDto>())
                .ToList()
                .AsReadOnly());
    }

    public async Task<Result<ReopenedSavingAccountDto>> ReopenAccount(Guid accountId)
    {
        var account = await _db.SavingAccounts.IgnoreQueryFilters()
            .Where(x => x.Id == accountId)
            .SingleOrDefaultAsync();

        if (account is null)
        {
            return Result<ReopenedSavingAccountDto>.Failure(Error.NotFound(nameof(Models.Domain.SavingAccount),
                accountId.ToString()));
        }

        account.Status = AccountStatus.Active;
        account.ClosedOn = null;

        await _db.SaveChangesAsync();

        return Result<ReopenedSavingAccountDto>.Success(new ReopenedSavingAccountDto(true));
    }
}