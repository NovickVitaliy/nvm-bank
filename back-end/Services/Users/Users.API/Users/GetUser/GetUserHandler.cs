using Common.CQRS.Handlers;
using Common.CQRS.Requests;
using Common.ErrorHandling;
using FluentValidation;
using Users.API.Data.Repository;
using Users.API.Models.Dtos;

namespace Users.API.Users.GetUser;

public record GetUserQuery(Guid Id) : IQuery<GetUserResult>;

public record GetUserResult(Result<UserDto> Result);

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty.");
    }
}

public class GetUserHandler : IQueryHandler<GetUserQuery, GetUserResult>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<GetUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _usersRepository.Get(request.Id, isReadOnly: true);
        
        return new GetUserResult(result);
    }
}