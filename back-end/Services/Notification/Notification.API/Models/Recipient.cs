namespace Notification.API.Models;

public class Recipient
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}