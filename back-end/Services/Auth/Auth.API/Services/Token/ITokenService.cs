using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services.Token;

public interface ITokenService
{
    string GenerateJwtToken(IdentityUser<Guid> user, List<Claim> claims, CancellationToken cancellationToken = default);
}