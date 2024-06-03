using Common.Messaging.Events;
using MassTransit;
using MimeKit.Text;
using Notification.API.Models;
using Notification.API.Services.Emailing;

namespace Notification.API.Consumers;

public class UserClosedCheckingAccountConsumer : IConsumer<UserClosedCheckingAccount>
{
    private readonly IEmailService _emailService;

    public UserClosedCheckingAccountConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<UserClosedCheckingAccount> context)
    {
        var recipient = new Recipient
        {
            Email = context.Message.Email,
        };
        
        await _emailService.SendEmail(recipient, new MessageDetails
        {
            Subject = "Checking Account Closed",
            Body =
                $"Your checking account with id of {context.Message.AccountId} has been closed. You have 14 days to reopen the account if you wish to.",
            Format = TextFormat.Plain
        });
    }
}