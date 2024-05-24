using Carter;
using MediatR;
using Users.API.Models.Dtos;

namespace Users.API.Users.UpdateUser;

public record UpdateUserRequest(UserDto User);

public record UpdateUserResponse(UserDto User);

public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{email}", async (string email, UpdateUserRequest req, ISender sender) =>
        {
            //TODO: 1) map request to UpdateUserCommand -> 2) send command to handler -> 3) return result to user based on the response
        });
    }
}