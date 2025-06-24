using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record Webhook
{
    /// <summary>
    /// Public endpoint that we send notifications to.
    /// </summary>
    [JsonPropertyName("uri")]
    public required string Uri { get; set; }

    /// <summary>
    /// String that we send with a notification so that you can ensure it is a valid notification from our gateway. We include the value in the Payroc-Secret header parameter in the webhook call.
    /// **Note:** In the response, we truncate the secret to the last 12 characters and mask the first 6 characters.
    /// </summary>
    [JsonPropertyName("secret")]
    public required string Secret { get; set; }

    /// <summary>
    /// Email address of the person or team that we contact if we can't deliver notifications.
    /// </summary>
    [JsonPropertyName("supportEmailAddress")]
    public required string SupportEmailAddress { get; set; }

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
