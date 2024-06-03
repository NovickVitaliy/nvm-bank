using Common.Messaging.Events;
using MassTransit;
using MimeKit.Text;
using Notification.API.Models;
using Notification.API.Services.Emailing;

namespace Notification.API.Consumers;

public class ClosedCheckingAccountDeletedPermanentlyConsumer : IConsumer<ClosedCheckingAccountDeletedPermanently>
{
    private readonly IEmailService _emailService;

    public ClosedCheckingAccountDeletedPermanentlyConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<ClosedCheckingAccountDeletedPermanently> context)
    {
        var message = context.Message;

        var recipient = new Recipient
        {
            Email = message.OwnerEmail
        };
        var messageDetails = new MessageDetails
        {
            Subject = "Checking Account Deleted",
            Body = $"You closed account with number of {message.AccountNumber} has been deleted and now cannot be reopened.",
            Format = TextFormat.Plain
        };
        await _emailService.SendEmail(recipient, messageDetails);
    }
}