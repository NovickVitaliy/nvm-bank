namespace Common.Messaging.Events;

public class UserSoftDeleted
{
    public required string Email { get; init; }
}