using Common.ErrorHandling;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Users.API.Models.Domain;
using Users.API.Models.Dtos;

namespace Users.API.Data.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly UsersDbContext _db;

    public UsersRepository(UsersDbContext db)
    {
        _db = db;
    }

    public async Task<Result<UserDto>> Get(Guid id, bool isReadOnly)
    {
        var userQueryable = _db.Users
            .Include(x => x.PhoneNumbers);

        var user = isReadOnly
            ? await userQueryable.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync()
            : await userQueryable.SingleOrDefaultAsync();

        if (user is null)
        {
            return Result<UserDto>.Failure(Error.NotFound(nameof(User), id.ToString()));
        }

        var userDto = user.Adapt<UserDto>();

        return Result<UserDto>.Success(userDto);
    }

    public async Task<Result<Guid>> Create(UserDto userDto)
    {
        if (await DoesUserWithGivenEmailExist(userDto.Email))
        {
            return Result<Guid>.Failure(Error.BadRequest("User with given email already exists."));
        }

        foreach (var phoneNumber in userDto.PhoneNumbers)
        {
            if (await IsPhoneNumberTaken(phoneNumber))
            {
                return Result<Guid>.Failure(Error.BadRequest($"Phone number {phoneNumber} is already taken."));
            }
        }
        
        var user = userDto.Adapt<User>();

        await _db.Users.AddAsync(user);

        await _db.SaveChangesAsync();
        
        return Result<Guid>.Success(user.Id);
    }

    private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
    {
        return await _db.PhoneNumbers.AnyAsync(x => x.Number == phoneNumber);
    }

    private async Task<bool> DoesUserWithGivenEmailExist(string email)
    {
        return await _db.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<(Result<bool>, string)> Delete(Guid id)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null)
        {
            return (Result<bool>.Failure(Error.NotFound(nameof(User), id.ToString())), string.Empty);
        }

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();

        return (Result<bool>.Success(true), user.Email);
    }

    public async Task<Result<bool>> Update(Guid id, UserDto userDto)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null)
        {
            return Result<bool>.Failure(Error.NotFound(nameof(User), id.ToString()));
        }

        user.Adapt(userDto);

        await _db.SaveChangesAsync();
        
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeletePhoneNumber(Guid userId, string phoneNumber)
    {
        var phone = await _db.PhoneNumbers.SingleOrDefaultAsync(x => x.Number == phoneNumber && x.UserId == userId);
        
        if (phone is null)
        {
            return Result<bool>.Failure(Error.NotFound(nameof(PhoneNumber), phoneNumber));
        }

        _db.PhoneNumbers.Remove(phone);

        await _db.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}