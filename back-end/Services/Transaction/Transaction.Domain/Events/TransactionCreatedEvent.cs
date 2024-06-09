using Transaction.Domain.Abstractions;

namespace Transaction.Domain.Events;

public class TransactionCreatedEvent : IDomainEvent
{
    public Guid Id { get; init; }
    public DateTime OccuredOn { get; init; }
    public string EventType { get; init; }
    public Models.Transaction Transaction { get; init; }
    public TransactionCreatedEvent(Models.Transaction transaction)
    {
        Id = Guid.NewGuid();
        OccuredOn = DateTime.Now;
        EventType = nameof(TransactionCreatedEvent);
        Transaction = transaction;
    }
}