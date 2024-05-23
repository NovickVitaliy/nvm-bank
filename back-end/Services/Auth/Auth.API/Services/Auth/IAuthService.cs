using Auth.API.Models.Dtos;
using Common.ErrorHandling;

namespace Auth.API.Services.Auth;

public interface IAuthService
{
    Task<Result<TokenDto>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default);
    Task<Result<string>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
}