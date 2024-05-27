namespace Common.Messaging.Events.Base;

public abstract class BaseEvent
{
    public Guid Id { get; private init; }
    public DateTime OccuredOn { get; private init; }

    protected BaseEvent()
    {
        Id = Guid.NewGuid();
        OccuredOn = DateTime.Now;
    }
}