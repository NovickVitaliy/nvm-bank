namespace Common.Messaging.Events.UserExists;

public record CheckUserExistence
{
    public required string Email { get; init; }
}