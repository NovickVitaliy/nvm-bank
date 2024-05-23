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

    public async Task<Result<TokenDto>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(registerDto.Email);

        if (user is not null)
        {
            return Result<TokenDto>.Failure(Error.Conflict("User with given email already exists"));
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
            return Result<TokenDto>.Success(token);
        }

        return Result<TokenDto>.Failure(Error.BadRequest(string.Join("\n",
            registrationResult.Errors.Select(x => x.Description))));
    }

    public async Task<Result<TokenDto>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return Result<TokenDto>.Failure(Error.BadRequest("User with given email does not exist."));
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordCorrect)
        {
            return Result<TokenDto>.Failure(Error.Unauthorized("Invalid password."));
        }

        var claims = await _userManager.GetClaimsAsync(user);
        var token = _tokenService.GenerateJwtToken(user, claims.ToList(), cancellationToken);

        return Result<TokenDto>.Success(token);
    }
}