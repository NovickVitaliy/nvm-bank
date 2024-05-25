using Common.ErrorHandling;
using Users.API.Models.Dtos;

namespace Users.API.Data.Repository;

public interface IUsersRepository
{
    Task<Result<UserDto>> Get(string email, bool isReadOnly);
    Task<Result<Guid>> Create(UserDto userDto);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<bool>> Update(Guid id, UserDto userDto);
}