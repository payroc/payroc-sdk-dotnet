using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the refund.
/// </summary>
[Serializable]
public record RefundOrder : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A unique identifier assigned by the merchant.
    /// </summary>
    [JsonPropertyName("orderId")]
    public required string OrderId { get; set; }

    /// <summary>
    /// Date and time that our gateway processed the refund. The value follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Description of the transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Amount of the refund. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("dccOffer")]
    public DccOffer? DccOffer { get; set; }

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
