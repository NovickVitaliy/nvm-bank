using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Data.Repository;

namespace Users.API.Users.DeletePhoneNumber;

public record DeletePhoneNumberCommand(Guid UserId, string PhoneNumber) : ICommand<DeletePhoneNumberResult>;

public record DeletePhoneNumberResult(Result<bool> Result);

public class DeletePhoneNumberHandler : ICommandHandler<DeletePhoneNumberCommand, DeletePhoneNumberResult>
{
    private readonly IUsersRepository _usersRepository;

    public DeletePhoneNumberHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<DeletePhoneNumberResult> Handle(DeletePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var result = await _usersRepository.DeletePhoneNumber(request.UserId, request.PhoneNumber);

        return new DeletePhoneNumberResult(result);
    }
}