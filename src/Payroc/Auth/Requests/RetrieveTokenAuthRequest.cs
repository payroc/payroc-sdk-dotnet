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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
