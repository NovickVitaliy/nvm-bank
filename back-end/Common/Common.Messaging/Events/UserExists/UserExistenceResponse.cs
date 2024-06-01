namespace Common.Messaging.Events.UserExists;

public record UserExistenceResponse
{
    public required bool Exists { get; init; }
}