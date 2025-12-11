using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains surcharge information. Our gateway returns this object only if the merchant adds a surcharge to transactions.
/// </summary>
[Serializable]
public record Surcharging : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the merchant can add a surcharge when the customer uses this card.
    /// </summary>
    [JsonPropertyName("allowed")]
    public required bool Allowed { get; set; }

    /// <summary>
    /// Surcharge amount to add to the transaction.
    /// **Note:** Our gateway returns the surcharge amount only if you include a transaction amount in the request.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// Surcharge rate that the merchant configures on their account.
    /// </summary>
    [JsonPropertyName("percentage")]
    public double? Percentage { get; set; }

    /// <summary>
    /// Statement that informs the customer about the surcharge fee.
    /// </summary>
    [JsonPropertyName("disclosure")]
    public string? Disclosure { get; set; }

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
