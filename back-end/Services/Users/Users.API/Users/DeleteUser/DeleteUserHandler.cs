using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;

namespace Users.API.Users.DeleteUser;

public record DeleteUserCommand(string Email) : ICommand<DeleteUserResult>;
public record DeleteUserResult(Result<bool> Result);

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand, DeleteUserResult>
{
    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: delete user using user repository

        return new DeleteUserResult(Result<bool>.Success(false));
    }
}