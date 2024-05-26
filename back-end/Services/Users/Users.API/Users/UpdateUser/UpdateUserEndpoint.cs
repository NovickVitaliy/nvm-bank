using Carter;
using Common.ErrorHandling;
using Mapster;
using MediatR;
using Users.API.Models.Dtos;

namespace Users.API.Users.UpdateUser;

public record UpdateUserRequest(UserDto User);

public record UpdateUserResponse(bool IsSuccess);

public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id}", async (Guid id, UpdateUserRequest req, ISender sender) =>
        {
            var cmd = new UpdateUserCommand(id, req.User);

            var result = await sender.Send(cmd);

            if (result.Result.IsFailure)
            {
                return result.Result.Error.Code switch
                {
                    400 => Results.BadRequest(result.Result.Error),
                    404 => Results.NotFound(result.Result.Error),
                    _ => Results.BadRequest("Unexpected Error Occured")
                };
            }

            return Results.Ok(new UpdateUserResponse(result.Result.Value!));
        }).RequireAuthorization();
    }
}