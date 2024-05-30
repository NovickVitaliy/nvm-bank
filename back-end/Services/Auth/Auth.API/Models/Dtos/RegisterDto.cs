namespace Auth.API.Models.Dtos;

public record RegisterDto(
    string Email,
    string Password,
    string ConfirmPassword);