using Common.Messaging.Events.Base;

namespace Common.Messaging.Events;

public class ClosedCheckingAccountDeletedPermanently : BaseEvent
{
    public required string OwnerEmail { get; init; }
    public required Guid AccountNumber { get; init; }
}