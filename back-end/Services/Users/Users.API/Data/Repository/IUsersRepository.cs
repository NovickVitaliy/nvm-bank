using Common.ErrorHandling;
using Users.API.Models.Dtos;

namespace Users.API.Data.Repository;

public interface IUsersRepository
{
    Task<Result<UserDto>> Get(string email, bool isReadOnly);
    Task<Result<UserDto>> Create(UserDto userDto);
    Task<Result<bool>> Delete(string email);
    Task<Result<bool>> Update(Guid id, UserDto userDto);
}