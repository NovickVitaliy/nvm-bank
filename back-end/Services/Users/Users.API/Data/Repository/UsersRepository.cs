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
        const string query =
            "SELECT u.\"Id\", u.\"FirstName\", u.\"LastName\", " +
            "u.\"Email\", u.\"DateOfBirth\", u.\"Gender\"," +
            " u.\"Address_Country\", u.\"Address_State\", " +
            "u.\"Address_StreetAddress\", u.\"Address_ZipCode\", " +
            "pn.\"Number\", pn.\"UserId\" FROM \"Users\" AS u " +
            "INNER JOIN \"PhoneNumbers\" AS pn " +
            "ON u.\"Id\" = pn.\"UserId\" " +
            "WHERE u.\"Email\" = {0}";

        var userQueryable = _db.Users
            .FromSqlRaw(query, email)
            .Include(x => x.PhoneNumbers);

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

    public async Task<Result<Guid>> Create(UserDto userDto)
    {
        if (await DoesUserWithGivenEmailExist(userDto.Email))
        {
            return Result<Guid>.Failure(Error.BadRequest("User with given email already exists."));
        }

        var user = userDto.Adapt<User>();
        user.Id = Guid.NewGuid();
        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            await _db.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Users\" (\"Id\", \"FirstName\", \"LastName\",\"DateOfBirth\", \"Email\", \"Gender\"," +
                " \"Address_Country\", \"Address_State\", \"Address_StreetAddress\", \"Address_ZipCode\")" +
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
            return Result<Guid>.Failure(Error.BadRequest(e.Message));
        }

        return Result<Guid>.Success(user.Id);
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

    public async Task<Result<bool>> Update(Guid id, UserDto userDto)
    {
        var user = await _db.Users.FromSqlRaw("SELECT * FROM \"Users\" WHERE \"Id\" = {0}", id).SingleOrDefaultAsync();

        if (user is null)
        {
            return Result<bool>.Failure(Error.NotFound(nameof(User), id.ToString()));
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            await _db.Database
                .ExecuteSqlRawAsync("UPDATE \"Users\" SET " +
                                    "\"FirstName\" = @p0, " +
                                    "\"LastName\" = @p1, " +
                                    "\"DateOfBirth\" = @p2, " +
                                    "\"Email\" = @p3, " +
                                    "\"Gender\" = @p4, " +
                                    "\"Address_Country\" = @p5, " +
                                    "\"Address_State\" = @p6, " +
                                    "\"Address_StreetAddress\" = @p7, " +
                                    "\"Address_ZipCode\" = @p8 " +
                                    "WHERE \"Id\" = @p9", userDto.FirstName, userDto.LastName,
                    userDto.DateOfBirth,
                    userDto.Email, userDto.Gender, userDto.Address.Country,
                    userDto.Address.State, userDto.Address.StreetAddress, userDto.Address.ZipCode, id);

            foreach (var phoneNumber in userDto.PhoneNumbers)
            {
                var phoneForUserExists = await _db.PhoneNumbers
                    .FromSqlRaw("SELECT * FROM \"PhoneNumbers\"" +
                                "WHERE \"Number\" = {0} AND \"UserId\" = {1}",
                        phoneNumber, id).SingleOrDefaultAsync() != null;

                if (!phoneForUserExists)
                {
                    var phoneId = Guid.NewGuid();
                    await _db.Database.ExecuteSqlRawAsync(
                        "INSERT INTO \"PhoneNumbers\" (\"Id\", \"Number\", \"UserId\") " +
                        "VALUES (@p0, @p1, @p2)", phoneId, phoneNumber, id);
                }
            }

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Result<bool>.Failure(Error.BadRequest(e.Message));
        }
        
        return Result<bool>.Success(true);
    }
}