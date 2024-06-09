using Transaction.Domain.Abstractions;
using Transaction.Domain.Constants;
using Transaction.Domain.Events;
using Transaction.Domain.ValueObjects;

namespace Transaction.Domain.Models;

public class Transaction : Aggregate<TransactionId>
{
    public DateTime OccuredOn { get; private set; }
    public AccountId Source { get; private set; }
    public AccountId Destination { get; private set; }
    public double Amount { get; private set; }
    public string Currency { get; private set; }
    public double AdditionalFee { get; private set; }
    public string DestinationBank { get; private set; }

    public Transaction(AccountId source, AccountId destination, double amount, string currency, double additionalFee,
        string destinationBank)
    {
        Id = new TransactionId(Guid.NewGuid());
        OccuredOn = DateTime.Now;
        Source = source;
        Destination = destination;
        Amount = amount;
        Currency = currency;
        AdditionalFee = additionalFee;
        DestinationBank = destinationBank;
    }

    public static Transaction Create(AccountId source, AccountId destination, double amount, string currency, 
        string destinationBank)
    {
        ArgumentException.ThrowIfNullOrEmpty(source.Value.ToString());
        ArgumentException.ThrowIfNullOrEmpty(destination.Value.ToString());
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        ValidateCurrency(currency);
        var additionalFee = CalculateAdditionalFee(amount, destinationBank);

        var transaction = new Transaction(
                source,
                destination,
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