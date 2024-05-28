using MimeKit.Text;

namespace Notification.API.Models;

public class MessageDetails
{
    public required string Subject { get; init; }
    public required string Body { get; init; }
    public required TextFormat Format { get; init; }
}