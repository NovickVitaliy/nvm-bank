using Microsoft.AspNetCore.Identity;

namespace Auth.API.Models.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public bool RegistrationFinished { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}