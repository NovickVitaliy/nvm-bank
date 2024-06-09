using Transaction.Domain.Abstractions;
using Transaction.Domain.Constants;
using Transaction.Domain.Events;
using Transaction.Domain.ValueObjects;

namespace Transaction.Domain.Models;

public class Transaction : Aggregate<TransactionId>
{
    public DateTime OccuredOn { get; private set; }
    public AccountNumber Source { get; private set; }
    public AccountType SourceType { get; private set; }
    public AccountNumber Destination { get; private set; }
    public AccountType DestinationType { get; private set; }
    public double Amount { get; private set; }
    public string Currency { get; private set; }
    public double AdditionalFee { get; private set; }
    public string DestinationBank { get; private set; }

    private Transaction(AccountNumber source, AccountType sourceType, AccountNumber destination,
        AccountType destinationType, double amount, string currency, double additionalFee, string destinationBank)
    {
        Id = new TransactionId(Guid.NewGuid());
        OccuredOn = DateTime.Now;
        Source = source;
        SourceType = sourceType;
        Destination = destination;
        DestinationType = destinationType;
        Amount = amount;
        Currency = currency;
        AdditionalFee = additionalFee;
        DestinationBank = destinationBank;
    }

    public static Transaction Create(AccountNumber source, AccountType sourceType, AccountNumber destination,
        AccountType destinationType, double amount, string currency,
        string destinationBank)
    {
        ArgumentException.ThrowIfNullOrEmpty(source.Value.ToString());
        ArgumentException.ThrowIfNullOrEmpty(destination.Value.ToString());
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        ValidateCurrency(currency);
        var additionalFee = CalculateAdditionalFee(amount, destinationBank);

        var transaction = new Transaction(
            source,
            sourceType,
            destination,
            destinationType,
            amount,
            currency,
            additionalFee,
            destinationBank);

        transaction.AddDomainEvent(new TransactionCreatedEvent(transaction));

        return transaction;
    }

    private static double CalculateAdditionalFee(double amount, string destinationBank)
    {
        double sum = 0;

        if (!destinationBank.Equals(DomainConstants.NativeBank))
        {
            sum += amount * DomainConstants.FeeForTransactionToOtherBank;
        }

        return sum;
    }

    private static void ValidateCurrency(string currency)
    {
        if (currency.Length != 3)
        {
            throw new ArgumentException("Invalid currency");
        }
    }
}