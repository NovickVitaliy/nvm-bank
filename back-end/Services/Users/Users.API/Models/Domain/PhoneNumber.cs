using Users.API.Interfaces;

namespace Users.API.Models.Domain;

public class PhoneNumber : ISoftDelete
{
    public Guid Id { get; set; }
    public required string Number { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsDeleted { get; set; } = false;
}