using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.API.Services.Token;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtOptions;

    public TokenService(IOptionsSnapshot<JwtSettings> _options)
    {
        _jwtOptions = _options.Value;
    }

    public string GenerateJwtToken(IdentityUser<Guid> user, List<Claim> claims,
        CancellationToken cancellationToken = default)
    {
        claims.AddRange([
            new(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()),
        ]);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer:_jwtOptions.Issuer,
            audience:_jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.LifetimeInMinutes),
            signingCredentials: signingCredentials,
            notBefore:DateTime.UtcNow);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}