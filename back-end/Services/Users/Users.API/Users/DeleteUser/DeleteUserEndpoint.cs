using Carter;
using MediatR;

namespace Users.API.Users.DeleteUser;


public record DeleteUserResponse(bool IsSuccess);

public class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{id}", async (Guid id, ISender sender) =>
        {
            var cmd = new DeleteUserCommand(id);

            var result = await sender.Send(cmd);

            if (result.Result.IsFailure)
            {
                return Results.NotFound(new DeleteUserResponse(false));
            }

            return Results.Ok(new DeleteUserResponse(true));
        });
    }
}