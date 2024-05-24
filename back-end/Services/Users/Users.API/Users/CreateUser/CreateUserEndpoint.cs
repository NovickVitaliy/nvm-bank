using Carter;
using MediatR;
using Users.API.Models.Dtos;

namespace Users.API.Users.CreateUser;

public record CreateUserRequest(UserDto User);

public record CreateUserResponse(UserDto User);

public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (CreateUserRequest req, ISender sender) =>
        {
            //TODO: 1) map request to CreateUserCommand -> 2) send command to handler -> 3) return response to user based on the result
        });
    }
}