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
            //TODO: 1) create GetUserQuery -> 2) send query to handler -> 3) return response to user based on the result
        });
    }
}