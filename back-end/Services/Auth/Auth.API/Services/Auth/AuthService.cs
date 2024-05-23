using Auth.API.Models.Dtos;
using Auth.API.Services.Token;
using Common.ErrorHandling;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly ITokenService _tokenService;
    
    public AuthService(UserManager<IdentityUser<Guid>> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(registerDto.Email);

        if (user is not null)
        {
            return Result<string>.Failure(Error.Conflict("User with given email already exists"));
        }

        user = new IdentityUser<Guid>()
        {
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            UserName = registerDto.Email,
        };

        var registrationResult = await _userManager.CreateAsync(user, registerDto.Password);

        if (registrationResult.Succeeded)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var token = _tokenService.GenerateJwtToken(user, claims.ToList(), cancellationToken);
            return Result<string>.Success(token);
        }

        return Result<string>.Failure(Error.BadRequest(string.Join("\n",
            registrationResult.Errors.Select(x => x.Description))));
    }

    public Task<Result<string>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}