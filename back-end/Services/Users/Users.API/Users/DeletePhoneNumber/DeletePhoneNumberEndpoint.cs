using Carter;
using Common.ApiResponses;
using Common.Extensions;
using MediatR;
using Users.API.Users.UpdateUser;

namespace Users.API.Users.DeletePhoneNumber;

public record DeletePhoneNumberResponse : BaseHttpResponse<bool>
{
    public DeletePhoneNumberResponse(bool value) : base(value)
    { }

    public DeletePhoneNumberResponse() : base(value:default)
    {
        
    }
}

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

            return result.Result.ToHttpResponse<bool, DeletePhoneNumberResponse>();
        });
    }
}