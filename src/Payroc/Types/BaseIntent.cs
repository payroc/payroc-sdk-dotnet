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
    [JsonPropertyName("id")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public string? Id { get; set; }

    /// <summary>
    /// Creation date of the pricing intent.
    /// </summary>
    [JsonPropertyName("createdDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date of the most recent change to the pricing intent.
    /// </summary>
    [JsonPropertyName("lastUpdatedDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? LastUpdatedDate { get; set; }

    /// <summary>
    /// Status of the pricing intent.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
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
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
