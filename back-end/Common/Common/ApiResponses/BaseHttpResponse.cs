using System.Text.Json.Serialization;

namespace Common.ApiResponses;

public abstract record BaseHttpResponse<TResultValue>
{
    public TResultValue Value { get; init; }
    
    protected BaseHttpResponse(TResultValue value)
    {
        Value = value;
    }
}