using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Data.Repository;
using Users.API.Models.Dtos;

namespace Users.API.Users.UpdateUser;

public record UpdateUserCommand(Guid Id, UserDto User) : ICommand<UpdateUserResult>;

public record UpdateUserResult(Result<bool> Result);

public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly IUsersRepository _usersRepository;

    public UpdateUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _usersRepository.Update(request.Id, request.User);
        
        return new UpdateUserResult(result);
    }
}