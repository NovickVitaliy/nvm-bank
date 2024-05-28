namespace Notification.API.Settings;

public class EmailSettings
{
    public const string Position = "EmailSettings";
    
    public required string Server { get; init; }
    public required int Port { get; init; }
    public required string SenderName { get; init; }
    public required string SenderEmail { get; init; }
    public required string Password { get; init; }
    public required string Username { get; init; }
}