using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Common.Messaging.Events;
using Common.Messaging.Events.UserCreated;
using FluentValidation;
using MassTransit;
using Users.API.Data.Repository;
using Users.API.Helpers;
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
            .NotEmpty().WithMessage("Phone Numbers cannot be empty.")
            .Must(OnlyOneMainPhoneGiven).WithMessage("Only one phone number can be made as main.");

        RuleFor(x => x.User.PhoneNumbers.Length)
            .LessThanOrEqualTo(UsersConstants.MaximumNumberOfPhoneNumbers)
            .WithMessage("You can create only 5 phone numbers for you account.");

        RuleForEach(x => x.User.PhoneNumbers)
            .ChildRules(x =>
            {
                x.RuleFor(dto => dto.Number)
                    .NotEmpty().WithMessage(dto => $"Phone number '{dto.Number}' cannot be empty.")
                    .Length(10).WithMessage(dto => $"Phone number '{dto.Number}' must have lenght of 10 characters.")
                    .Must(DoesConsistOfOnlyDigits)
                    .WithMessage(dto => $"Phone number {dto.Number} must consist only of digits.");
            });
    }

    private static bool DoesConsistOfOnlyDigits(
        PhoneNumberDto phoneNumberDto, 
        string number,
        ValidationContext<PhoneNumberDto> context)
        => number.All(char.IsDigit);


    private static bool OnlyOneMainPhoneGiven(PhoneNumberDto[] phoneNumbers)
    {
        return phoneNumbers.Count(x => x.IsMain) == 1;
    }
}

public class CreateUserHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRequestClient<UserCreatedEvent> _requestClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateUserHandler(
        IUsersRepository usersRepository,
        IRequestClient<UserCreatedEvent> requestClient,
        IPublishEndpoint publishEndpoint)
    {
        _usersRepository = usersRepository;
        _requestClient = requestClient;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _usersRepository.Create(request.User);
        if (result.IsSuccess)
        {
            var response = await _requestClient.GetResponse<UserCreatedResponse>(new UserCreatedEvent()
            {
                UserEmail = request.User.Email,
                MainPhoneNumber = request.User.PhoneNumbers.Single(x => x.IsMain).Number,
            }, cancellationToken);

            if (!response.Message.Success)
            {
                await _usersRepository.Delete(result.Value);
                result = Result<Guid>.Failure(Error.BadRequest(response.Message.Description));
            }

            await _publishEndpoint.Publish(new UserFinishedRegistration
            {
                Email = request.User.Email,
                FirstName = request.User.FirstName,
                LastName = request.User.LastName
            }, cancellationToken);
        }

        return new CreateUserResult(result);
    }
}