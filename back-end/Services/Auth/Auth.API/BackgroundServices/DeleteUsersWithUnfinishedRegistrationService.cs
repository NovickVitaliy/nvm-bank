using Auth.API.Data;
using Auth.API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.BackgroundServices;

public class DeleteUsersWithUnfinishedRegistrationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _periodToCheckUsers = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _timeToFinishRegistration = TimeSpan.FromMinutes(60);

    public DeleteUsersWithUnfinishedRegistrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_periodToCheckUsers);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            await using var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

            var users = await db.Users.FromSqlRaw("SELECT * FROM Users " +
                                                  "WHERE RegistrationFinished = 0 " +
                                                  "AND DATEDIFF(minute, CreatedAt, SYSDATETIME()) >= {0}",
                    _timeToFinishRegistration.Minutes)
                .ToListAsync(cancellationToken: stoppingToken);

            foreach (var applicationUser in users)
            {
                await db.Users.FromSqlRaw("SELECT Id FROM Users " +
                                          "WHERE Id = {0}", applicationUser.Id.ToString())
                    .ExecuteDeleteAsync(stoppingToken);
            }
        }
    }
}