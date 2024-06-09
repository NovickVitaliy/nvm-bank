using Transaction.Domain.Abstractions;

namespace Transaction.Domain.ValueObjects;

public struct AccountNumber : IStronglyTypedId
{
    public Guid Value { get; }

    public AccountNumber(Guid value)
    {
        Value = value;
    }
}