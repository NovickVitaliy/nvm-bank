using System.Net;

namespace Common.ErrorHandling;

public sealed record Error(int Code, string Description)
{
    public static Error None => new((int)HttpStatusCode.OK, string.Empty);
    public static Error NotFound(string entityName, string id) 
        => new((int)HttpStatusCode.NotFound, $"Entity '{entityName}' with id of '{id}' was not found");
}