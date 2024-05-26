using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;
using Users.API.Data.Repository;

namespace Users.API.Users.DeleteUser;

public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;
public record DeleteUserResult(Result<bool> Result);

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty.");
    }
}

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand, DeleteUserResult>
{
    private readonly IUsersRepository _usersRepository;

    public DeleteUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = _usersRepository.Delete(request.Id);
        return new DeleteUserResult(Result<bool>.Success(false));
    }
}