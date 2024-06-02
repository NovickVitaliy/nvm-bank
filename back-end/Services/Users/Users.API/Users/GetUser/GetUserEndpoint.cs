using Carter;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models.Dtos;

namespace Users.API.Users.GetUser;

public record GetUserResponse : BaseHttpResponse<UserDto>
{
    public GetUserResponse(UserDto value) : base(value)
    { }

    public GetUserResponse() : this(value:default)
    {
        
    }
}

public class GetUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id}", async (Guid id, ISender sender) =>
        {
            var cmd = new GetUserQuery(id);

            var result = await sender.Send(cmd);

            return result.Result.ToHttpResponse<UserDto, GetUserResponse>();
        }).RequireAuthorization();
    }
}