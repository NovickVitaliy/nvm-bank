namespace Users.API.Models.Dtos;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Email,
    PhoneNumberDto[] PhoneNumbers,
    string Gender,
    AddressDto Address);