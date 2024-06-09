namespace Transaction.Application.Dto;

public record CreateTransactionDto(
    Guid Source,
    Guid Destination,
    double Amount,
    string Currency,
    string DestinationBank
);