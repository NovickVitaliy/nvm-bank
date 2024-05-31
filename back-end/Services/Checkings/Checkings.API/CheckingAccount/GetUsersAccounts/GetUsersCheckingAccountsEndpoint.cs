using Carter;
using Checkings.API.Models.Dtos;
using MediatR;

namespace Checkings.API.CheckingAccount.GetUsersAccounts;


public record GetUsersCheckingAccountsResponse(IReadOnlyCollection<CheckingAccountDto> CheckingAccounts);

public class GetUsersCheckingAccountsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/checkings/users/{userEmail}/accounts", 
            async (string userEmail, ISender sender) =>
            {
                var query = new GetUsersCheckingAccountsQuery(userEmail);

                var result = await sender.Send(query);
            });
    }
}