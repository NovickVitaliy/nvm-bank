using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auth.API.Settings;

public record PasswordSettings(
    [Required] int RequiredLength,
    [Required] bool RequireNonAlphanumeric,
    [Required] bool RequireDigit,
    [Required] bool RequireLowercase,
    [Required] bool RequireUppercase)
{
    public const string Position = "PasswordSettings";

    public PasswordSettings() : this(default, default, default, default, default)
    {
    }
}

public static class PasswordPatterns
{
    public const string PhoneNumber = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
    public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).+$\n";
}
