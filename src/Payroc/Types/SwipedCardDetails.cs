using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer’s card details for swiped transactions.
/// </summary>
public record SwipedCardDetails
{
    /// <summary>
    /// If an offline transaction is not approved using the initial entry method, reprocess the transaction using a downgraded entry method.
    /// For example, a swiped transaction can be downgraded to a keyed transaction.
    /// </summary>
    [JsonPropertyName("downgradeTo")]
    public SwipedCardDetailsDowngradeTo? DowngradeTo { get; set; }

    [JsonPropertyName("swipedData")]
    public required SwipedCardDetailsSwipedData SwipedData { get; set; }

    /// <summary>
    /// Cardholder’s name.
    /// </summary>
    [JsonPropertyName("cardholderName")]
    public string? CardholderName { get; set; }

    /// <summary>
    /// Cardholder’s signature. For more information about the format of the signature, see Special Fields and Parameters.
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

    [JsonPropertyName("pinDetails")]
    public SwipedCardDetailsPinDetails? PinDetails { get; set; }

    [JsonPropertyName("ebtDetails")]
    public EbtDetailsWithVoucher? EbtDetails { get; set; }

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
