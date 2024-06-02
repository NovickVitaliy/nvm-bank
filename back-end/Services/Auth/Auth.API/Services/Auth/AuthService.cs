using Auth.API.Models.Dtos;
using Auth.API.Models.Identity;
using Auth.API.Services.Token;
using Common.ErrorHandling;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    
    public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
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

        user = new ApplicationUser()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            CreatedAt = DateTime.Now
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
            return Result<TokenDto>.Failure(Error.NotFound("User", loginDto.Email));
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