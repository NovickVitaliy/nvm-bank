using System.Security.Claims;
using Auth.API.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services.Token;

public interface ITokenService
{
    TokenDto GenerateJwtToken(IdentityUser<Guid> user, List<Claim> claims, CancellationToken cancellationToken = default);
}