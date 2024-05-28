using Notification.API.Models;

namespace Notification.API.Services.Emailing;

public interface IEmailService
{
    Task SendEmail(Recipient recipient, MessageDetails messageDetails);
}