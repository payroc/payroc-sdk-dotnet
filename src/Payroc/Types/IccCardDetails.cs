using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Integrated Circuit Card (ICC).
/// </summary>
public record IccCardDetails
{
    /// <summary>
    /// If an offline transaction is not approved using the initial entry method, reprocess the transaction using a downgraded entry method.
    /// For example, an Integrated Circuit Card (ICC) transaction can be downgraded to a swiped transaction or a keyed transaction.
    /// </summary>
    [JsonPropertyName("downgradeTo")]
    public IccCardDetailsDowngradeTo? DowngradeTo { get; set; }

    [JsonPropertyName("device")]
    public required EncryptionCapableDevice Device { get; set; }

    /// <summary>
    /// EMV tags in Tag-Length-Value (TLV) format.
    /// </summary>
    [JsonPropertyName("iccData")]
    public required string IccData { get; set; }

    /// <summary>
    /// First digit of the card number.
    /// </summary>
    [JsonPropertyName("firstDigitOfPan")]
    public string? FirstDigitOfPan { get; set; }

    /// <summary>
    /// Cardholder’s signature. For more information about the format of the signature, see Special Fields and Parameters.
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

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
