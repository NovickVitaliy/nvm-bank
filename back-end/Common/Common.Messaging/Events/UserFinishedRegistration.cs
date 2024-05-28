using Common.Messaging.Events.Base;

namespace Common.Messaging.Events;

public class UserFinishedRegistration : BaseEvent
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}