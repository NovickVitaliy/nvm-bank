using Common.Messaging.Events.Base;

namespace Common.Messaging.Events.UserCreated;

public class UserCreatedEvent : BaseEvent
{
    public required string UserEmail { get; init; }
    public required string MainPhoneNumber { get; init; }
}