using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Models.Dtos;

namespace Users.API.Users.CreateUser;

public record CreateUserCommand(UserDto User) : ICommand<CreateUserResult>;

public record CreateUserResult(Result<UserDto> Result);

public class CreateUserHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: Create user using user repository

        return new CreateUserResult(Result<UserDto>.Success(request.User));
    }
}