using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Auth;

public record GetTokenAuthRequest
{
    /// <summary>
    /// The API key of the application
    /// </summary>
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
