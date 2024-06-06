using Common.Messaging.Events;
using MassTransit;
using MimeKit.Text;
using Notification.API.Models;
using Notification.API.Services.Emailing;

namespace Notification.API.Consumers;

public class UserClosedBankingAccountConsumer : IConsumer<UserClosedBankingAccount>
{
    private readonly IEmailService _emailService;

    public UserClosedBankingAccountConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<UserClosedBankingAccount> context)
    {
        var recipient = new Recipient
        {
            Email = context.Message.Email,
        };
        
        await _emailService.SendEmail(recipient, new MessageDetails
        {
            Subject = "Checking Account Closed",
            Body =
                $"Your checking account with number of {context.Message.AccountNumber} has been closed. You have 14 days to reopen the account if you wish to.",
            Format = TextFormat.Plain
        });
    }
}