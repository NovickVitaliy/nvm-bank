using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using FluentValidation;
using MassTransit;
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
    private readonly IPublishEndpoint _publishEndpoint;
    public DeleteUserHandler(IUsersRepository usersRepository, IPublishEndpoint publishEndpoint)
    {
        _usersRepository = usersRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var (result, email) = await _usersRepository.Delete(request.Id);
        if (result.IsSuccess)
        {
            await _publishEndpoint.Publish(new UserSoftDeleted()
            {
                Email = email
            }, cancellationToken);
        }
        return new DeleteUserResult(Result<bool>.Success(false));
    }
}