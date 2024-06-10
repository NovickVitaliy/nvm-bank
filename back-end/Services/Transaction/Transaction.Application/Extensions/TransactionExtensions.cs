using Transaction.Application.Dto;

namespace Transaction.Application.Extensions;

public static class TransactionExtensions
{
    public static TransactionDto ToTransactionDto(this Domain.Models.Transaction transaction)
        => new TransactionDto(
            transaction.Id.Value,
            transaction.Source.Value,
            transaction.Destination.Value,
            transaction.Amount,
            transaction.Currency,
            transaction.DestinationBank,
            transaction.AdditionalFee,
            transaction.OccuredOn);
}