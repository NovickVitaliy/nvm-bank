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

    public async Task<Result<UserDto>> Get(string email, bool isReadOnly)
    {
        const string query = "SELECT * FROM \"Users\" AS u " +
                             "INNER JOIN \"PhoneNumbers\" AS pn " +
                             "ON u.\"Id\" = pn.\"UserId\" " +
                             "WHERE u.\"Email\" = {0}";

        var userQueryable = _db.Users.FromSqlRaw(query, email);

        var user = isReadOnly
            ? await userQueryable.AsNoTracking().SingleOrDefaultAsync()
            : await userQueryable.SingleOrDefaultAsync();

        if (user is null)
        {
            return Result<UserDto>.Failure(Error.NotFound(nameof(User), email));
        }

        var userDto = user.Adapt<UserDto>();

        return Result<UserDto>.Success(userDto);
    }

    public async Task<Result<UserDto>> Create(UserDto userDto)
    {
        if (await DoesUserWithGivenEmailExist(userDto.Email))
        {
            return Result<UserDto>.Failure(Error.BadRequest("User with given email already exists."));
        }

        var user = userDto.Adapt<User>();
        user.Id = Guid.NewGuid();
        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            await _db.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Users\" (\"Id\", \"FirstName\", \"LastName\",\"DateOfBirth\", \"Email\", \"Gender\", \"Address_Country\", \"Address_State\", \"Address_StreetAddress\", \"Address_ZipCode\")" +
                "VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)", user.Id, user.FirstName, user.LastName,
                user.DateOfBirth, user.Email, user.Gender, user.Address.Country, user.Address.State,
                user.Address.StreetAddress, user.Address.ZipCode);

            foreach (var pn in user.PhoneNumbers)
            {
                pn.Id = Guid.NewGuid();
                pn.UserId = user.Id;
                await _db.Database.ExecuteSqlRawAsync("INSERT INTO \"PhoneNumbers\" (\"Id\", \"Number\", \"UserId\") " +
                                                      "VALUES (@p0, @p1, @p2)", pn.Id, pn.Number, pn.UserId);
            }

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Result<UserDto>.Failure(Error.BadRequest(e.Message));
        }

        return Result<UserDto>.Success(userDto);
    }

    private async Task<bool> DoesUserWithGivenEmailExist(string email)
    {
        return (await _db.Users.FromSqlRaw("SELECT * FROM \"Users\" WHERE \"Email\" = {0}", email)
            .SingleOrDefaultAsync()) != null;
    }

    public async Task<Result<bool>> Delete(string email)
    {
        var rowsAffected = await _db.Users.FromSqlRaw("DELETE FROM \"Users\" WHERE \"Email\" = {0}", email)
            .ExecuteDeleteAsync();

        return rowsAffected == 0
            ? Result<bool>.Failure(Error.NotFound(nameof(User), email))
            : Result<bool>.Success(true);
    }

    public async Task<Result<UserDto>> Update(UserDto userDto)
    {
        if (await DoesUserWithGivenEmailExist(userDto.Email))
        {
            return Result<UserDto>.Failure(Error.NotFound(nameof(User), userDto.Email));
        }

        await _db.Database.ExecuteSqlRawAsync("UPDATE \"Users\" SET " +
                                              "\"FirstName\" = @p0, " +
                                              "\"LastName\" = @p1, " +
                                              "\"DateOfBirth\" = @p2, " +
                                              "\"Email\" = @p3, " +
                                              "\"Gender\" = @p4, " +
                                              "\"Address_Country\" = @p5, " +
                                              "\"Address_State\" = @p6, " +
                                              "\"Address_StreetAddress\" = @p7, " +
                                              "\"Address_ZipCode\" = @p8 " +
                                              "WHERE \"Email\" = @p9", userDto.FirstName, userDto.LastName,
            userDto.DateOfBirth,
            userDto.Email, userDto.Gender, userDto.Address.Country,
            userDto.Address.State, userDto.Address.StreetAddress, userDto.Address.ZipCode, userDto.Email);

        return Result<UserDto>.Success(userDto);
    }
}