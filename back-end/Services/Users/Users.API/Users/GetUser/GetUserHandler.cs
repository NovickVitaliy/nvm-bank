using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using Users.API.Data.Repository;
using Users.API.Models.Dtos;

namespace Users.API.Users.GetUser;

public record GetUserQuery(string Email) : IQuery<GetUserResult>;

public record GetUserResult(Result<UserDto> Result);

public class GetUserHandler : IQueryHandler<GetUserQuery, GetUserResult>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<GetUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _usersRepository.Get(request.Email, isReadOnly: true);
        
        return new GetUserResult(result);
    }
}