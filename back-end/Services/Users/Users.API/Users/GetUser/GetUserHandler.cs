using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Models.Dtos;

namespace Users.API.Users.GetUser;

public record GetUserQuery : IQuery<GetUserResult>;

public record GetUserResult(Result<UserDto> Result);

public class GetUserHandler : IQueryHandler<GetUserQuery, GetUserResult>
{
    public async Task<GetUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        //TODO: Get user using user repository

        return new GetUserResult(Result<UserDto>.Success(default!));
    }
}