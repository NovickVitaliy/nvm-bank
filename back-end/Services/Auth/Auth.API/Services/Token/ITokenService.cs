using System.Security.Claims;
using Auth.API.Models.Dtos;
using Auth.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services.Token;

public interface ITokenService
{
    TokenDto GenerateJwtToken(ApplicationUser user, List<Claim> claims, CancellationToken cancellationToken = default);
}