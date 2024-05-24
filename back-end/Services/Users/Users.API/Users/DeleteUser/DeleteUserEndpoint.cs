using Carter;
using MediatR;

namespace Users.API.Users.DeleteUser;


public record DeleteUserResponse(bool IsSuccess);

public class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{email}", async (string email, ISender sender) =>
        {
            //TODO: 1) create command to delete user -> 2) send command to handler -> 3) return response to user based on the result
        });
    }
}