using Microsoft.AspNetCore.Http;

namespace Common.ApiResponses;

public abstract class BaseApiResponse
{
    public required int StatusCode { get; init; }
    public required string Type { get; init; }
    public required string Title { get; init; }
    public required string Path { get; init; }

    public abstract Task WriteToResponseAsJsonAsync(HttpResponse response);
    public abstract Task WriteToResponseAsync(HttpResponse response);
}