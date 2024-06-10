namespace Transaction.Application.Dto;

public record CreateTransactionDto(
    Guid Source,
    string SourceType,
    Guid Destination,
    string DestinationType,
    double Amount,
    string Currency,
    string DestinationBank
);