using Carter;
using Common.ApiResponses;
using Common.Extensions;
using Mapster;
using MediatR;
using Users.API.Authorization;
using Users.API.Models.Dtos;

namespace Users.API.Users.CreateUser;

public record CreateUserRequest(UserDto User);

public record CreateUserResponse : BaseHttpResponse<Guid>
{
    public CreateUserResponse(Guid value) : base(value)
    { }

    public CreateUserResponse() : this(value:default)
    {
        
    }
}

public class CreateUserEndpoint : ICarterModule
{
    public  void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (CreateUserRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<CreateUserCommand>();

            var result = await sender.Send(cmd);

            return result.Result.ToHttpResponse<Guid, CreateUserResponse>();
        }).RequireAuthorization(Policies.MatchEmailInTokenAndBodyPolicy);
    }
}