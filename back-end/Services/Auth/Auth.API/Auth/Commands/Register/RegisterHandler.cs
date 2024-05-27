using System.Text.RegularExpressions;
using Auth.API.Models.Dtos;
using Auth.API.Services.Auth;
using Auth.API.Settings;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Auth.API.Auth.Commands.Register;

public record RegisterCommand(RegisterDto RegisterDto) : ICommand<RegisterResult>;

public record RegisterResult(Result<TokenDto> Result);

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IOptions<PasswordSettings> options)
    {
        var passwordSettings = options.Value;

        RuleFor(x => x.RegisterDto)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("RegisterDto cannot be empty");

        RuleFor(x => x.RegisterDto.Email)
            .NotEmpty().WithMessage("'Email Address' cannot be empty.")
            .EmailAddress().WithMessage("'Email Address' must be a proper email address.");

        RuleFor(x => x.RegisterDto.PhoneNumber)
            .NotEmpty().WithMessage("'Phone Number' cannot be empty.")
            .MinimumLength(10).WithMessage("'Phone Number' must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("'Phone Number' must not exceed 50 characters.")
            .Matches(new Regex(PasswordPatterns.PhoneNumber)).WithMessage("'Phone Number' is not valid.");

        RuleFor(x => x.RegisterDto.Password)
            .NotEmpty().WithMessage("'Password' cannot be empty.")
            .MinimumLength(passwordSettings.RequiredLength)
            .WithMessage("'Password' must have at least {MinLength} characters.");
        
        RuleFor(x => x.RegisterDto.ConfirmPassword)
            .NotEmpty().WithMessage("'Confirm Password' cannot be empty.")
            .Equal(x => x.RegisterDto.Password).WithMessage("{PropertyName} must be equal to {ComparisonProperty}.");
    }
}

public class RegisterHandler : ICommandHandler<RegisterCommand, RegisterResult>
{
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var token = await _authService.RegisterAsync(request.RegisterDto, cancellationToken);

        return new RegisterResult(token);
    }
}