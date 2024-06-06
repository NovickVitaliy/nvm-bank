using Common.Messaging.Events;
using MassTransit;
using MimeKit.Text;
using Notification.API.Models;
using Notification.API.Services.Emailing;

namespace Notification.API.Consumers;

public class UserOpenedBankingAccountConsumer : IConsumer<UserOpenedBankingAccount>
{
    private readonly IEmailService _emailService;

    public UserOpenedBankingAccountConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<UserOpenedBankingAccount> context)
    {
        var message = context.Message;
        var recipient = new Recipient
        {
            Email = message.Email
        };

        var messageDetails = new MessageDetails
        {
            Subject = "Newly opened account",
            Body = $"New {message.AccountType} was opened with number of {message.AccountNumber}",
            Format = TextFormat.Plain
        };

        await _emailService.SendEmail(recipient, messageDetails);
    }
}