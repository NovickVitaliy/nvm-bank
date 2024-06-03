using Common.Messaging.Events.Base;

namespace Common.Messaging.Events;

public class UserClosedCheckingAccount : BaseEvent
{
    public required Guid AccountId { get; init; }
    public required string Email { get; init; }
}