using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Data.Repository;
using Users.API.Models.Dtos;

namespace Users.API.Users.CreateUser;

public record CreateUserCommand(UserDto User) : ICommand<CreateUserResult>;

public record CreateUserResult(Result<Guid> Result);

public class CreateUserHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUsersRepository _usersRepository;

    public CreateUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return new CreateUserResult(await _usersRepository.Create(request.User));
    }
}