using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Users.API.Users.CreateUser;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Users.API.Authorization.MatchEmailPolicy;

public class MatchEmailHandler : AuthorizationHandler<MatchEmailRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MatchEmailHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MatchEmailRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            context.Fail();
            return;
        }

        var emailFromToken = httpContext.User.FindFirst(x => x.Type == ClaimTypes.Email);

        if (string.IsNullOrEmpty(emailFromToken?.Value))
        {
            context.Fail();
            return;
        }

        httpContext.Request.EnableBuffering();

        var bodyStream = new StreamReader(httpContext.Request.Body);
        var body = await bodyStream.ReadToEndAsync();
        httpContext.Request.Body.Position = 0;
        var request = JsonSerializer.Deserialize<CreateUserRequest>(body, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        if (request?.User is null)
        {
            context.Fail();
            return;
        }
        
        if (string.IsNullOrEmpty(request.User.Email) || request.User.Email != emailFromToken.Value)
        {
            context.Fail();
            return;
        }
        
        context.Succeed(requirement);
    }
}