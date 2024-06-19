using Checkings.API.Data;
using Common.Messaging.Events.CheckAccountExistance;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.CheckingAccount.Consumers;

public class CheckCheckingAccountExistanceConsumer : IConsumer<CheckCheckingAccountExistance> {
    private readonly CheckingsDbContext _db;

    public CheckCheckingAccountExistanceConsumer(CheckingsDbContext db) {
        _db = db;
    }

    public async Task Consume(ConsumeContext<CheckCheckingAccountExistance> context) {
        var msg = context.Message;

        var account = await _db.CheckingAccounts
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