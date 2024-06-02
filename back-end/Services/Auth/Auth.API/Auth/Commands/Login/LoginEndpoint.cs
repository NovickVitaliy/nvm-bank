using Auth.API.Models.Dtos;
using Carter;
using Common.ApiResponses;
using Common.Extensions;
using Mapster;
using MediatR;

namespace Auth.API.Auth.Commands.Login;

public record LoginRequest(LoginDto LoginDto);

public record LoginResponse : BaseHttpResponse<TokenDto>
{
    public LoginResponse(TokenDto value) : base(value)
    {
        
    }

    public LoginResponse() : this(value:default!)
    {
        
    }
}

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async (LoginRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<LoginCommand>();

            var result = await sender.Send(cmd);

            return result.Result.ToHttpResponse<TokenDto, LoginResponse>();
        });
    }
}