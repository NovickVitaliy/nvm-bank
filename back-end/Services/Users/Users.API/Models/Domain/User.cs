using Users.API.Interfaces;

namespace Users.API.Models.Domain;

public class User : ISoftDelete
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];
    public required string Gender { get; set; } = string.Empty;
    public required Address Address { get; set; }
    public bool IsDeleted { get; set; } = false;
}