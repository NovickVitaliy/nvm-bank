using Checkings.API.Data;
using Common.Messaging.Commands.WithdrawMoney;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.CheckingAccount.Consumers;

public class WithdrawMoneyFromCheckingAccountConsumer : IConsumer<WithdrawMoneyFromCheckingAccount> {
    private readonly CheckingsDbContext _db;

    public WithdrawMoneyFromCheckingAccountConsumer(CheckingsDbContext db) {
        _db = db;
    }

    public async Task Consume(ConsumeContext<WithdrawMoneyFromCheckingAccount> context) {
        var msg = context.Message;

        var account = await _db.CheckingAccounts.SingleAsync(x => x.AccountNumber == msg.AccountNumber);

        account.Balance -= msg.Amount;

        await _db.SaveChangesAsync();

        await context.RespondAsync(new WithdrawMoneyResult(true));
    }
}