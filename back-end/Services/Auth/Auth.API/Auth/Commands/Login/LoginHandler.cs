using Auth.API.Models.Dtos;
using Auth.API.Services.Auth;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;

namespace Auth.API.Auth.Commands.Login;

public record LoginCommand(LoginDto LoginDto) : ICommand<LoginResult>;

public record LoginResult(Result<TokenDto> Result);

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.LoginDto)
            .NotNull().WithMessage("LoginDto cannot be null.");

        RuleFor(x => x.LoginDto.Email)
            .NotEmpty().WithMessage("'Email Address' cannot be empty.")
            .EmailAddress().WithMessage("'Email Address' must be valid email address.");

        RuleFor(x => x.LoginDto.Password)
            .NotEmpty().WithMessage("'Password' cannot be empty.");
    }
}

public class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IAuthService _authService;

    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request.LoginDto, cancellationToken);

        return new LoginResult(result);
    }
}