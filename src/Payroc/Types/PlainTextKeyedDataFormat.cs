using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the plain-text card data for keyed transactions.
/// </summary>
[Serializable]
public record PlainTextKeyedDataFormat : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("device")]
    public Device? Device { get; set; }

    /// <summary>
    /// Customer’s card number.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public required string CardNumber { get; set; }

    /// <summary>
    /// Expiry date of the customer’s card.
    /// **Note:** We require you to send an expiry date for most BIN lookups and electronic voucher transactions.
    /// </summary>
    [JsonPropertyName("expiryDate")]
    public string? ExpiryDate { get; set; }

    /// <summary>
    /// Security code of the customer’s card.
    /// </summary>
    [JsonPropertyName("cvv")]
    public string? Cvv { get; set; }

    /// <summary>
    /// Issue number of the customer’s card.
    /// </summary>
    [JsonPropertyName("issueNumber")]
    public string? IssueNumber { get; set; }

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
