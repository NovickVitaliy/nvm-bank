using Carter;
using MediatR;

namespace Users.API.Users.UpdateUser;

public record DeletePhoneNumberResponse(bool IsSuccess);

public class DeletePhoneNumberEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{userId}/phone-numbers/{phoneNumber}", 
            async (
                Guid userId, 
                string phoneNumber, 
                ISender sender) =>
        {
            var cmd = new DeletePhoneNumberCommand(userId, phoneNumber);

            var result = await sender.Send(cmd);

            if (result.Result.IsFailure)
            {
                return Results.NotFound(result.Result.Error);
            }

            return Results.Ok(new DeletePhoneNumberResponse(result.Result.Value));
        });
    }
}