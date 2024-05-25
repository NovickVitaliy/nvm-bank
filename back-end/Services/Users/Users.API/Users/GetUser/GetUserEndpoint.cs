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
        app.MapGet("/users/{email}", async (string email, ISender sender) =>
        {
            var cmd = new GetUserQuery(email);

            var result = await sender.Send(cmd);

            if (result.Result.IsFailure)
            {
                
            }

            return Results.Ok(result.Result.Value);
        });
    }
}