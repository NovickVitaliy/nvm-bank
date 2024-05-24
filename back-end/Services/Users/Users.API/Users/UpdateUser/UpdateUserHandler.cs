using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Models.Dtos;

namespace Users.API.Users.UpdateUser;

public record UpdateUserCommand(UserDto User) : ICommand<UpdateUserResult>;

public record UpdateUserResult(Result<UserDto> Result);

public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: update user using user repository

        return new UpdateUserResult(Result<UserDto>.Success(null));
    }
}