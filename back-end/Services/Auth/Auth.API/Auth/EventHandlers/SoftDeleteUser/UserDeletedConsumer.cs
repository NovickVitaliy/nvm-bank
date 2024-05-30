using Auth.API.Models.Identity;
using Common.Messaging.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Auth.EventHandlers.SoftDeleteUser;

public class UserDeletedConsumer : IConsumer<UserSoftDeleted>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserDeletedConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<UserSoftDeleted> context)
    {
        var message = context.Message;
        var user = await _userManager.FindByEmailAsync(message.Email);

        user!.IsDeleted = true;

        await _userManager.UpdateAsync(user);
    }
}