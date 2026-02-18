using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the total funds available to the merchant.
/// </summary>
[Serializable]
public record MerchantBalance : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that the processor assigned to the merchant.
    /// </summary>
    [JsonPropertyName("merchantId")]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Total funding balance for the merchant, including pending amounts. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("funds")]
    public int? Funds { get; set; }

    /// <summary>
    /// Amount of the funding balance that we have not yet sent to funding accounts. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("pending")]
    public int? Pending { get; set; }

    /// <summary>
    /// Amount of the funding balance that you can use in funding instructions. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("available")]
    public int? Available { get; set; }

    /// <summary>
    /// Currency of the funding balance. We return a value of `USD`.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

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
