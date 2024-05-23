using System.ComponentModel.DataAnnotations;

namespace Auth.API.Settings;

public record JwtSettings(
    [Required]string Issuer,
    [Required]string Audience,
    [Required]string Secret,
    [Required]int LifetimeInMinutes
)
{
    public JwtSettings() : this(null, null, null, 0)
    {
        
    }
}