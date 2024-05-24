namespace Users.API.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];
    public string Gender { get; set; } = string.Empty;
    public Address Address { get; set; }
}