using Mapster;
using Users.API.Models.Domain;
using Users.API.Models.Dtos;

namespace Users.API.MappingConfiguration;

public static class MapsterConfiguration
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<UserDto, User>.NewConfig().MapWith(src => new User()
        {
            FirstName = src.FirstName,
            LastName = src.LastName,
            Address = new Address()
            {
                Country = src.Address.Country,
                State = src.Address.State,
                StreetAddress = src.Address.StreetAddress,
                ZipCode = src.Address.ZipCode
            },
            Email = src.Email,
            Gender = src.Gender,
            DateOfBirth = src.DateOfBirth,
            PhoneNumbers = src.PhoneNumbers.Select(x => new PhoneNumber()
            {
                Number = x.Number,
                IsMain = x.IsMain,
            }).ToList()
        });

        TypeAdapterConfig<User, UserDto>.NewConfig().MapWith(src => new UserDto(
                src.Id,
                src.FirstName,
                src.LastName,
                src.DateOfBirth,
                src.Email,
                src.PhoneNumbers.Select(x => new PhoneNumberDto(x.Number, x.IsMain)).ToArray(),
                src.Gender,
                new AddressDto(src.Address.Country, src.Address.Country,
                    src.Address.ZipCode, src.Address.StreetAddress)));
    }
}