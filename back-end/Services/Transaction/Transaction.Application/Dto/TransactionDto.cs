namespace Transaction.Application.Dto;

public record TransactionDto(
    Guid Id,
    Guid Source,
    Guid Destination,
    double Amount,
    string Currency,
    string DestinationBank,
    double AdditionalFee,
    DateTime OccuredOn
    );