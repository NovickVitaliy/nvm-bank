namespace Users.API.Models.Dtos;

public record AddressDto(
        string Country,
        string State,
        string ZipCode,
        string StreetAddress);