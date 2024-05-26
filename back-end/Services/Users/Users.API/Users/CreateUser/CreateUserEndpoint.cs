using Carter;
using Mapster;
using MediatR;
using Users.API.Models.Dtos;

namespace Users.API.Users.CreateUser;

public record CreateUserRequest(UserDto User);

public record CreateUserResponse(Guid Id);

public class CreateUserEndpoint : ICarterModule
{
    public  void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (CreateUserRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<CreateUserCommand>();

            var response = await sender.Send(cmd);

            if (response.Result.IsFailure)
            {
                return Results.BadRequest(response.Result.Error);
            }
            
            return Results.Created($"/users/{response.Result.Value}",
                new CreateUserResponse(response.Result.Value));
        }).RequireAuthorization();
    }
}