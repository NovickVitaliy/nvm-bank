using Common.Messaging.Events.UserExists;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Users.API.Data;

namespace Users.API.Users.EventHandlers.CheckUserExistence;

public class CheckUserExistenceConsumer : IConsumer<Common.Messaging.Events.UserExists.CheckUserExistence>
{
    private readonly UsersDbContext _db;

    public CheckUserExistenceConsumer(UsersDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<Common.Messaging.Events.UserExists.CheckUserExistence> context)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.Email == context.Message.Email);

        await context.RespondAsync(new UserExistenceResponse()
        {
            Exists = user is not null
        });
    }
}