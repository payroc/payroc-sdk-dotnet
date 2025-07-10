using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the details of the payment card.
/// </summary>
[Serializable]
public record Card : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Card brand that the card is linked to. For example, Visa.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Method that the device used to capture the card details.
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public required CardEntryMethod EntryMethod { get; set; }

    /// <summary>
    /// Cardholder’s name.
    /// </summary>
    [JsonPropertyName("cardholderName")]
    public string? CardholderName { get; set; }

    /// <summary>
    /// Cardholder’s signature.
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

    /// <summary>
    /// Masked card number. Our gateway shows only the first six digits and the last four digits of the card number, for example, 548010******5929.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public required string CardNumber { get; set; }

    /// <summary>
    /// Expiry date of the customer's card. The format is in **MMYY**.
    /// </summary>
    [JsonPropertyName("expiryDate")]
    public required string ExpiryDate { get; set; }

    [JsonPropertyName("secureToken")]
    public SecureTokenSummary? SecureToken { get; set; }

    [JsonPropertyName("securityChecks")]
    public SecurityCheck? SecurityChecks { get; set; }

    /// <summary>
    /// Array of emvTag objects.
    /// </summary>
    [JsonPropertyName("emvTags")]
    public IEnumerable<EmvTag>? EmvTags { get; set; }

    /// <summary>
    /// Array of cardBalance objects. Our gateway returns this array only when the customer uses an Electronic Benefit Transfer (EBT) card.
    /// </summary>
    [JsonPropertyName("balances")]
    public IEnumerable<CardBalance>? Balances { get; set; }

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
