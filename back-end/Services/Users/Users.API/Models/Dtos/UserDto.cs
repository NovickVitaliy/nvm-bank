namespace Users.API.Models.Dtos;

public record UserDto(
        string FirstName,
        string LastName,
        DateOnly DateOfBirth,
        string Email,
        string[] PhoneNumbers,
        string Gender,
        AddressDto Address);