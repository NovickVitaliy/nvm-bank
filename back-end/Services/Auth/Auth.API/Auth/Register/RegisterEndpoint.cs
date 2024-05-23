using Auth.API.Models.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Auth.API.Auth.Register;

public record RegisterRequest(RegisterDto RegisterDto);

public record RegisterResponse(string JwtToken);

public class RegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<RegisterCommand>();

            var result = await sender.Send(cmd);

            var response = result.Adapt<RegisterResponse>();

            return Results.Ok(response);
        });
    }
}