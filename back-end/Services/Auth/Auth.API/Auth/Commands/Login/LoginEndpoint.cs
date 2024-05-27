using Auth.API.Models.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Auth.API.Auth.Commands.Login;

public record LoginRequest(LoginDto LoginDto);

public record LoginResponse(TokenDto Token);

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async (LoginRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<LoginCommand>();

            var result = await sender.Send(cmd);

            if (result.Result.IsFailure)
            {
                return result.Result.Error.Code switch
                {
                    401 => Results.Unauthorized(),
                    400 => Results.BadRequest(new { description = result.Result.Error.Description }),
                    _ => Results.BadRequest(new { description = "An error occured." })
                };
            }

            return Results.Ok(result.Result.Value);
        });
    }
}