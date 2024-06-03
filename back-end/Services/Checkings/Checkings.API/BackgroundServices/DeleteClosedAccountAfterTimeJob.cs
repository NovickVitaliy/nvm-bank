using Checkings.API.Data;
using Common.Accounts;
using Common.Messaging.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Checkings.API.BackgroundServices;

public class DeleteClosedAccountAfterTimeJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DeleteClosedAccountAfterTimeJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = new TimeSpan(23, 59, 59) - DateTime.Now.TimeOfDay;

            await Task.Delay(delay, stoppingToken);

            await using var scope = _serviceProvider.CreateAsyncScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<CheckingsDbContext>();
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            
            var accountsToDelete = await dbContext.CheckingAccounts
                .Where(x => x.ClosedOn != null && (DateTime.UtcNow - x.ClosedOn).Value.Days >=
                    CheckingAccountConstants.DaysAccountClosedBeforeDeleted.Days)
                .ToListAsync(cancellationToken: stoppingToken);

            List<ClosedCheckingAccountDeletedPermanently> messages = [];
            messages.AddRange(accountsToDelete.Select(checkingAccount => new ClosedCheckingAccountDeletedPermanently
                { AccountNumber = checkingAccount.AccountNumber, OwnerEmail = checkingAccount.OwnerEmail }));

            var tasks = messages.Select(message => publishEndpoint.Publish(message, stoppingToken));
            dbContext.CheckingAccounts.RemoveRange(accountsToDelete);
            await Task.WhenAll(tasks);
        }
    }
}