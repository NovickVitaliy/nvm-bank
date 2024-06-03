using Common.Messaging.Events;
using MassTransit;
using MimeKit.Text;
using Notification.API.Models;
using Notification.API.Services.Emailing;

namespace Notification.API.Consumers;

public class UserFinishedRegistrationConsumer : IConsumer<UserFinishedRegistration>
{
    private readonly IEmailService _emailService;

    public UserFinishedRegistrationConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<UserFinishedRegistration> context)
    {
        var eventMessage = context.Message;

        var recipient = new Recipient
        {
            Email = eventMessage.Email,
        };
        
        var messageDetails = new MessageDetails
        {
            Subject = "Welcome to NVM-Bank",
            Body = $"Welcome the the NVM-Bank. We hope that you will have great time using our bank!",
            Format = TextFormat.Plain
        };

        await _emailService.SendEmail(recipient, messageDetails);
    }
}