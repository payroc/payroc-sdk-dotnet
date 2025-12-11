using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Auth;

[Serializable]
public record RetrieveTokenAuthRequest
{
    /// <summary>
    /// The API key of the application
    /// </summary>
    [JsonIgnore]
    public required string ApiKey { get; set; }

    /// <summary>
    /// The client ID of the application
    /// </summary>
    [JsonPropertyName("client_id")]
    public required string ClientId { get; set; }

    /// <summary>
    /// The client secret of the application
    /// </summary>
    [JsonPropertyName("client_secret")]
    public required string ClientSecret { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
