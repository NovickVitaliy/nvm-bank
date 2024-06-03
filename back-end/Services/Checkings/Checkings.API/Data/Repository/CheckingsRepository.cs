using Checkings.API.CheckingAccount.Commands.Open;
using Checkings.API.Models.Dtos;
using Common.Accounts.Status;
using Common.ErrorHandling;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.Data.Repository;

public class CheckingsRepository : ICheckingsRepository
{
    private readonly CheckingsDbContext _db;

    public CheckingsRepository(CheckingsDbContext db)
    {
        _db = db;
    }

    public async Task<Result<CheckingAccountOpenedDto>> OpenAccount(string ownerEmail, string currency)
    {
        var account = new Models.Domain.CheckingAccount
        {
            OwnerEmail = ownerEmail,
            AccountNumber = Guid.NewGuid(),
            Balance = 0,
            CreatedAt = DateTime.UtcNow,
            Status = AccountStatus.Active,
            Currency = currency
        };

        await _db.CheckingAccounts.AddAsync(account);

        await _db.SaveChangesAsync();

        return Result<CheckingAccountOpenedDto>.Success(new CheckingAccountOpenedDto(account.Id.ToString(), account.AccountNumber.ToString()));
    }

    public async Task<(Result<bool>, string)> CloseAccount(Guid id, bool isAware)
    {
        if (!isAware)
        {
            return (Result<bool>.Failure(
                Error.BadRequest("To close the account you must be aware of the possible consequences.")), string.Empty);
        }

        var checkingAccount = await _db.CheckingAccounts.FindAsync(id);

        if (checkingAccount is null)
        {
            return (Result<bool>.Failure(
                Error.BadRequest("Checking account is not found.")), string.Empty);
        }

        checkingAccount.Status = AccountStatus.Closed;

        await _db.SaveChangesAsync();
        
        return (Result<bool>.Success(true), checkingAccount.OwnerEmail);
    }

    public async Task<Result<CheckingAccountDto>> GetAccount(Guid id)
    {
        var account = await _db.CheckingAccounts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (account is null)
        {
            return Result<CheckingAccountDto>.Failure(Error.NotFound(nameof(Models.Domain.CheckingAccount), id.ToString()));
        }

        return Result<CheckingAccountDto>.Success(account.Adapt<CheckingAccountDto>());
    }

    public async Task<Result<AccountBalanceDto>> GetBalance(Guid id)
    {
        var balance = await _db.CheckingAccounts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new AccountBalanceDto(x.Id, x.Balance))
            .SingleOrDefaultAsync();

        if (balance is null)
        {
            return Result<AccountBalanceDto>.Failure(Error.NotFound(nameof(Models.Domain.CheckingAccount),
                id.ToString()));
        }


        return Result<AccountBalanceDto>.Success(balance);
    }

    public async Task<Result<IReadOnlyCollection<CheckingAccountDto>>> GetUsersAccounts(string ownerEmail)
    {
        var account = await _db.CheckingAccounts
            .AsNoTracking()
            .Where(x => x.OwnerEmail == ownerEmail)
            .ToListAsync();

        return Result<IReadOnlyCollection<CheckingAccountDto>>.Success(
            account
                .Select(x => x.Adapt<CheckingAccountDto>())
                .ToList()
                .AsReadOnly());
    }
}