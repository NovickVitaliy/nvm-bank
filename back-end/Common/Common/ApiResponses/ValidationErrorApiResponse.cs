using Microsoft.AspNetCore.Http;

namespace Common.ApiResponses;

public class ValidationErrorApiResponse : BaseApiResponse
{
    public required IDictionary<string, string[]> ValidationErrors { get; init; }
    
    public override async Task WriteToResponseAsJsonAsync(HttpResponse response)
    {
        response.StatusCode = StatusCode;
        await response.WriteAsJsonAsync(this);
    }

    public override async Task WriteToResponseAsync(HttpResponse response)
    {
        response.StatusCode = StatusCode;
        await response.WriteAsync(ToString() ?? string.Empty);
    }
}