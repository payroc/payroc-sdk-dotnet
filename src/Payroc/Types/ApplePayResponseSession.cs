using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record ApplePayResponseSession
{
    /// <summary>
    /// Object that Apple returns when they start the merchant's Apple Pay session. Use this object to retrieve the cardholder's encrypted payment details from Apple.
    /// </summary>
    [JsonPropertyName("startSessionResponse")]
    public required string StartSessionResponse { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
