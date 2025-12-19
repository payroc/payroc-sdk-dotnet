using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the base fees.
/// </summary>
[Serializable]
public record BaseIntent : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the pricing intent.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Date and time that we received your request to create the pricing intent. We return this value in the [ISO-8601](https://www.iso.org/iso-8601-date-and-time-format.html) format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date and time that the pricing intent was last modified. We return this value in the [ISO-8601](https://www.iso.org/iso-8601-date-and-time-format.html) format.
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
    /// Unique identifier that you can assign to the pricing intent for your own records.
    /// </summary>
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    /// <summary>
    /// [Metadata](https://docs.payroc.com/api/metadata) object that contains your custom data.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
