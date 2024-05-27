namespace Common.Messaging.Events.UserCreated;

public class UserCreatedResponse
{
    public required bool Success { get; init; }
    public required string Description { get; init; }
}