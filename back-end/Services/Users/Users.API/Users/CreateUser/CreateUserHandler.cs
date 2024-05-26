using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;
using Users.API.Data.Repository;
using Users.API.Models.Dtos;

namespace Users.API.Users.CreateUser;

public record CreateUserCommand(UserDto User) : ICommand<CreateUserResult>;

public record CreateUserResult(Result<Guid> Result);

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User)
            .NotNull().WithMessage("User cannot be null.");

        RuleFor(x => x.User.Address)
            .NotNull().WithMessage("Address cannot be null.");

        RuleFor(x => x.User.Address.StreetAddress)
            .NotEmpty().WithMessage("Street address cannot be empty.");

        RuleFor(x => x.User.Address.Country)
            .NotEmpty().WithMessage("Country cannot be empty.");

        RuleFor(x => x.User.Address.State)
            .NotEmpty().WithMessage("State cannot be empty.");

        RuleFor(x => x.User.Address.ZipCode)
            .NotEmpty().WithMessage("Zip Code cannot be empty.");

        RuleFor(x => x.User.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email must be proper email address.");

        RuleFor(x => x.User.Gender)
            .NotEmpty().WithMessage("Gender cannot be empty.");

        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First Name cannot be empty.");

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last Name cannot be empty.");

        RuleFor(x => x.User.DateOfBirth)
            .NotEmpty().WithMessage("Date Of Birth cannot be empty.");

        RuleFor(x => x.User.PhoneNumbers)
            .NotEmpty().WithMessage("Phone Numbers cannot be empty.");
    }
}

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