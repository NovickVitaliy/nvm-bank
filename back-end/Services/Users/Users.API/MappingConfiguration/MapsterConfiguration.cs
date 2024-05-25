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
                Number = x
            }).ToList()
        });
    }
}