using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models.Dtos;

namespace Users.API.Users.GetUser;

public record GetUserResponse(UserDto User);

public class GetUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id}", async (Guid id, ISender sender) =>
        {
            var cmd = new GetUserQuery(id);

            var result = await sender.Send(cmd);

            if (result.Result.IsFailure)
            {
                return Results.NotFound(new { description = "User was not found" });
            }

            return Results.Ok(result.Result.Value);
        });
    }
}