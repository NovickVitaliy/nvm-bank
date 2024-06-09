using Transaction.Domain.Abstractions;

namespace Transaction.Domain.ValueObjects;

public struct AccountId : IStronglyTypedId
{
    public Guid Value { get; }

    public AccountId(Guid value)
    {
        Value = value;
    }
}