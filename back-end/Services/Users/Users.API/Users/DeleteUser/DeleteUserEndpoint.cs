using Carter;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;

namespace Users.API.Users.DeleteUser;


public record DeleteUserResponse : BaseHttpResponse<bool>
{
    public DeleteUserResponse(bool value) : base(value)
    { }

    public DeleteUserResponse() : this(value:default)
    {
        
    }
}

public class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{id}", async (Guid id, ISender sender) =>
        {
            var cmd = new DeleteUserCommand(id);

            var result = await sender.Send(cmd);

            return result.Result.ToHttpResponse<bool, DeleteUserResponse>();
        }).RequireAuthorization("");
    }
}