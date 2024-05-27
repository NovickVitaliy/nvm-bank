using Auth.API.Models.Identity;
using Common.Messaging.Events;
using Common.Messaging.Events.UserCreated;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Auth.EventHandlers.FinishUserRegistration;

public class FinishUserRegistrationConsumer : IConsumer<UserCreatedEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<FinishUserRegistrationConsumer> _logger;
    public FinishUserRegistrationConsumer(UserManager<ApplicationUser> userManager, ILogger<FinishUserRegistrationConsumer> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        _logger.LogInformation("Accepter UserCreatedEvent.");
        var message = context.Message;

        var user = await _userManager.FindByEmailAsync(message.UserEmail);

        if (user is null)
        {
            await context.RespondAsync<UserCreatedResponse>(new UserCreatedResponse()
            {
                Description = "Session timed out.",
                Success = false
            });
            return;
        }

        user.RegistrationFinished = true;
        user.PhoneNumber = message.MainPhoneNumber;
        await _userManager.UpdateAsync(user);

        await context.RespondAsync(new UserCreatedResponse()
        {
            Description = "Registration finished successfuly.",
            Success = true
        });
    }
}