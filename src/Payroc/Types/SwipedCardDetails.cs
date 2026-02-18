using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer’s card details for swiped transactions.
/// </summary>
[Serializable]
public record SwipedCardDetails : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If an offline transaction is not approved using the initial entry method, reprocess the transaction using a downgraded entry method.
    /// For example, a swiped transaction can be downgraded to a keyed transaction.
    /// </summary>
    [JsonPropertyName("downgradeTo")]
    public SwipedCardDetailsDowngradeTo? DowngradeTo { get; set; }

    /// <summary>
    /// Polymorphic object that contains payment card details that a device captured from the magnetic strip.
    ///
    /// The value of the dataFormat parameter determines which variant you should use:
    /// -	`encrypted` - Payment card details are encrypted.
    /// -	`plainText` - Payment card details are in plain text.
    /// </summary>
    [JsonPropertyName("swipedData")]
    public required SwipedCardDetailsSwipedData SwipedData { get; set; }

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
    public SwipedCardDetailsPinDetails? PinDetails { get; set; }

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
