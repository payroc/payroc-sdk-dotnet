using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the keyed card details.
/// </summary>
[Serializable]
public record KeyedCardDetails : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Polymorphic object that contains payment card details that the merchant manually entered into the device.
    ///
    /// The value of the dataFormat parameter determines which variant you should use:
    /// -	`fullyEncrypted` - Some payment card details are encrypted.
    /// -	`partiallyEncrypted` - Payment card details are in plain text.
    /// -	`plainText` - All payment card details are encrypted.
    /// </summary>
    [JsonPropertyName("keyedData")]
    public required KeyedCardDetailsKeyedData KeyedData { get; set; }

    /// <summary>
    /// Cardholder’s name.
    /// </summary>
    [JsonPropertyName("cardholderName")]
    public string? CardholderName { get; set; }

    /// <summary>
    /// Cardholder's signature. For more information about how to format the signature, go to [How to send a signature to our gateway](https://docs.payroc.com/knowledge/basic-concepts/signature-capture).
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

    /// <summary>
    /// Polymorphic object that contains information about the customer's PIN.
    /// </summary>
    [JsonPropertyName("pinDetails")]
    public KeyedCardDetailsPinDetails? PinDetails { get; set; }

    [JsonPropertyName("ebtDetails")]
    public EbtDetailsWithVoucher? EbtDetails { get; set; }

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
