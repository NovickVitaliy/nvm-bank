using Auth.API.Models.Dtos;
using Carter;
using Common.ApiResponses;
using Common.ErrorHandling;
using Common.Extensions;
using Mapster;
using MediatR;

namespace Auth.API.Auth.Commands.Register;

public record RegisterRequest(RegisterDto RegisterDto);

public record RegisterResponse : BaseHttpResponse<TokenDto>
{
    public RegisterResponse(TokenDto value) : base(value)
    { }

    public RegisterResponse() : this(value:default!)
    { }
}

public class RegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<RegisterCommand>();

            var result = await sender.Send(cmd);


            return result.Result.ToHttpResponse<TokenDto, RegisterResponse>();
        });
    }
}