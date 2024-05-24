namespace Users.API.Models.Domain;

public record Address
{
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string StreetAddress { get; set; } = string.Empty;
}