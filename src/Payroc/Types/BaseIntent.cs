using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the base fees.
/// </summary>
public record BaseIntent
{
    /// <summary>
    /// Unique identifier of the pricing intent.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Date and time that we received your request to create the pricing intent in our system.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date and time that the pricing intent was last modified.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastUpdatedDate")]
    public DateTime? LastUpdatedDate { get; set; }

    /// <summary>
    /// Status of the pricing intent. The value can be one of the following:
    /// - `active` - We have approved the pricing intent.
    /// - `pendingReview` - We have not yet reviewed the pricing intent.
    /// - `rejected` - We have rejected the pricing intent.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public BaseIntentStatus? Status { get; set; }

    /// <summary>
    /// Unique identifier that you use to connect a merchant to the pricing intent.
    /// </summary>
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    /// <summary>
    /// [Metadata](/api/metadata) object that contains your custom data.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

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
