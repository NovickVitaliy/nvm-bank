namespace Transaction.Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T> where T : IStronglyTypedId
{
    
}

public interface IAggregate : IEntity
{
    public IEnumerable<IDomainEvent> DomainEvents { get; }
    public IDomainEvent[] ClearDomainEvents();
}