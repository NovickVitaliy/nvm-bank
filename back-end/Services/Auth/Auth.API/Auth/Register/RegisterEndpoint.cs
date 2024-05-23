using Auth.API.Models.Dtos;
using Carter;
using Common.ErrorHandling;
using Mapster;
using MediatR;

namespace Auth.API.Auth.Register;

public record RegisterRequest(RegisterDto RegisterDto);

public record RegisterResponse(Result<TokenDto> Result);

public class RegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<RegisterCommand>();

            var result = await sender.Send(cmd);

            return result.Result.IsFailure 
                ? Results.Conflict(result.Result.Error) 
                : Results.Ok(result.Result.Value);
        });
    }
}