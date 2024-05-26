using Common.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace Common.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException()
    {
    }

    protected BaseException(string? message) : base(message)
    {
    }

    public abstract BaseApiResponse ToApiResponse(string errorPath);
}