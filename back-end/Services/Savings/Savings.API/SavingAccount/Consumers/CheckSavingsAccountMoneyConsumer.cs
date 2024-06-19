using Common.Messaging.Events.CheckAccountMoney;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Savings.API.Data;

namespace Savings.API.SavingAccount.Consumers;

public class CheckSavingsAccountMoneyConsumer : IConsumer<CheckSavingsAccountMoney> {
    private readonly SavingDbContext _db;
    private readonly ILogger<CheckSavingsAccountMoneyConsumer> _logger;
    public CheckSavingsAccountMoneyConsumer(SavingDbContext db, ILogger<CheckSavingsAccountMoneyConsumer> logger) {
        _db = db;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CheckSavingsAccountMoney> context) {
        _logger.LogInformation("Handling request: {RequestName}", context.Message.GetType().Name);
        
        var msg = context.Message;

        var account = await _db.SavingAccounts
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