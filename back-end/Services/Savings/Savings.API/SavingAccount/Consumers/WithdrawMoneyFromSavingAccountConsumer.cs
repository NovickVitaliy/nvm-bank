using Common.Messaging.Commands.WithdrawMoney;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Savings.API.Data;

namespace Savings.API.SavingAccount.Consumers;

public class WithdrawMoneyFromSavingAccountConsumer : IConsumer<WithdrawMoneyFromSavingAccount> {
    private readonly SavingDbContext _db;

    public WithdrawMoneyFromSavingAccountConsumer(SavingDbContext db) {
        _db = db;
    }

    public async Task Consume(ConsumeContext<WithdrawMoneyFromSavingAccount> context) {
        var msg = context.Message;

        var account = await _db.SavingAccounts.SingleAsync(x => x.AccountNumber == msg.AccountNumber);

        account.Balance -= msg.Amount;

        await _db.SaveChangesAsync();

        await context.RespondAsync(new WithdrawMoneyResult(true));
    }
}