using Common.Messaging.Events.CheckAccountExistance;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Savings.API.Data;

namespace Savings.API.SavingAccount.EventHandlers;

public class CheckSavingAccountExistanceConsumer : IConsumer<CheckSavingAccountExistance> {
    private readonly SavingDbContext _db;

    public CheckSavingAccountExistanceConsumer(SavingDbContext db) {
        _db = db;
    }

    public async Task Consume(ConsumeContext<CheckSavingAccountExistance> context) {
        var msg = context.Message;

        var account = await _db.SavingAccounts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.AccountNumber == msg.AccountNumber);

        if (account is null) {
            await context.RespondAsync(new CheckAccountExistanceResult(false));
        }
        else {
            await context.RespondAsync(new CheckAccountExistanceResult(true));
        }
    }
}