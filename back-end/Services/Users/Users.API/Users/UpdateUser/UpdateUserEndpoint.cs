using Carter;
using Common.ApiResponses;
using Common.ErrorHandling;
using Common.Extensions;
using Mapster;
using MediatR;
using Users.API.Models.Dtos;

namespace Users.API.Users.UpdateUser;

public record UpdateUserRequest(UserDto User);

public record UpdateUserResponse : BaseHttpResponse<bool>
{
    public UpdateUserResponse(bool value) : base(value)
    {
    }

    public UpdateUserResponse() : this(value:default)
    {
        
    }
}

public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id}", async (Guid id, UpdateUserRequest req, ISender sender) =>
        {
            var cmd = new UpdateUserCommand(id, req.User);

            var result = await sender.Send(cmd);

            return result.Result.ToHttpResponse<bool, UpdateUserResponse>();
        }).RequireAuthorization();
    }
}