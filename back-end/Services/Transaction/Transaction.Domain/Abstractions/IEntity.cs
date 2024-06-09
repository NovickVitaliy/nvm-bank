namespace Transaction.Domain.Abstractions;

public interface IEntity<T> : IEntity where T : IStronglyTypedId
{
    T Id { get; set; }
}

public interface IEntity
{
    DateTime? CreatedAt { get; set; }
}