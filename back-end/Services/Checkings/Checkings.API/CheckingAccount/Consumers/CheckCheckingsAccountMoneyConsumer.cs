using Checkings.API.Data;
using Common.Messaging.Events.CheckAccountMoney;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.CheckingAccount.Consumers;

public class CheckCheckingsAccountMoneyConsumer : IConsumer<CheckCheckingAccountMoney> {
    private readonly CheckingsDbContext _db;
    private readonly ILogger<CheckCheckingsAccountMoneyConsumer> _logger;
    public CheckCheckingsAccountMoneyConsumer(CheckingsDbContext db, ILogger<CheckCheckingsAccountMoneyConsumer> logger) {
        _db = db;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CheckCheckingAccountMoney> context) {
        _logger.LogInformation("Handling request: {RequestName}", context.Message.GetType().Name);
        
        var msg = context.Message;

        var account = await _db.CheckingAccounts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.AccountNumber == msg.AccountNumber);

        if (account is null) {
            await context.RespondAsync(new CheckAccountMoneyResult(false, "Account with such number does not exist."));
            return;
        }

        var money = account.Balance;

        if (money >= msg.TransferAmount) {
            await context.RespondAsync(new CheckAccountMoneyResult(true));
        }
        else {
            await context.RespondAsync(new CheckAccountMoneyResult(false, "Not enough money on the balance"));
        }
    }
}