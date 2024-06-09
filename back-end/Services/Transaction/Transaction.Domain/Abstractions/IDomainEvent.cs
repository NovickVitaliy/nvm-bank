using MediatR;

namespace Transaction.Domain.Abstractions;

public interface IDomainEvent : INotification
{
     Guid Id { get; init; }
     DateTime OccuredOn { get; init; }
     string EventType { get; init; }
}