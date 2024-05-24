namespace Users.API.Models.Domain;

public class PhoneNumber
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; }
}