namespace Transaction.Domain.ValueObjects;

public struct AccountType
{
    public string Type { get; }

    public AccountType(string type)
    {
        Type = type;
    }
}