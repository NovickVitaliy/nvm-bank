namespace Transaction.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T> where T : IStronglyTypedId
{
    public DateTime? CreatedAt { get; set; }
    public T Id { get; set; }
}