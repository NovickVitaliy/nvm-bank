using Transaction.Domain.Abstractions;

namespace Transaction.Domain.ValueObjects;

public struct TransactionId : IStronglyTypedId
{
    public Guid Value { get; }

    public TransactionId(Guid value)
    {
        Value = value;
    }
}