namespace Transaction.Domain.Abstractions;

public interface IStronglyTypedId
{ 
    Guid Value { get; }
}