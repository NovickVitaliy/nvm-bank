using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.API.Models;
using Notification.API.Settings;

namespace Notification.API.Services.Emailing;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptionsSnapshot<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }
    public async Task SendEmail(Recipient recipient, MessageDetails messageDetails)
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        
        message.To.Add(new MailboxAddress($"{recipient.FirstName} {recipient.LastName}" ,
            recipient.Email));
        
        message.Subject = message.Subject;
        
        message.Body = new TextPart(messageDetails.Format)
        {
            Text = messageDetails.Body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}