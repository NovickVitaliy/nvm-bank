using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Application.Dto;
using Transaction.Application.Transactions.Commands.CreateTransaction;
using Transaction.Application.Transactions.Queries.GetTransaction;
using Transaction.Application.Transactions.Queries.GetTransactionsByAccount;

namespace Transaction.API.Controllers;

[Route("transactions")]
public class TransactionController : ControllerBase {
    private readonly ISender _sender;

    public TransactionController(ISender sender) {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateTransactionDto createTransactionDto) {
        var cmd = new CreateTransactionCommand(createTransactionDto);

        var result = await _sender.Send(cmd);

        return Ok(new { id = result.Result.Value });
    }

    [HttpGet("{transactionId:guid}")]
    public async Task<IActionResult> GetTransaction(Guid transactionId) {
        var query = new GetTransactionQuery(transactionId);

        var result = await _sender.Send(query);

        return result.Result.Error.Code switch {
            200 => Ok(result.Result.Value),
            404 => NotFound(result.Result.Error)
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsByAccount(Guid accountNumber) {
        var query = new GetTransactionsByAccountQuery(accountNumber);

        var result = await _sender.Send(query);

        return Ok(result.Result.Value);
    }
}