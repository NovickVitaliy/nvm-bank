using Auth.API.Models.Dtos;
using Auth.API.Services.Auth;
using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;

namespace Auth.API.Auth.Register;

public record RegisterCommand(RegisterDto RegisterDto) : ICommand<RegisterResult>;

public record RegisterResult(Result<string> JwtToken);


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