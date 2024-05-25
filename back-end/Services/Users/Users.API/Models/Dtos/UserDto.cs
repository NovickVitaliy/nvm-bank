namespace Users.API.Models.Dtos;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Email,
    string[] PhoneNumbers,
    string Gender,
    AddressDto Address);