using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the 3-D Secure information from a third party.
/// </summary>
[Serializable]
public record ThirdPartyThreeDSecure : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// E-commerce indicator (ECI) result of a the 3-D Secure check.
    /// </summary>
    [JsonPropertyName("eci")]
    public required ThirdPartyThreeDSecureEci Eci { get; set; }

    /// <summary>
    /// Unique transaction identifier that the merchant assigned to the transaction and sent in the authentication request.
    /// </summary>
    [JsonPropertyName("xid")]
    public string? Xid { get; set; }

    /// <summary>
    /// Cardholder Authentication Verification Value (CAVV) that the card issuer provided to prove that they authorized the online payment.
    /// </summary>
    [JsonPropertyName("cavv")]
    public string? Cavv { get; set; }

    /// <summary>
    /// Directory Server Transaction ID that the processor assigned to the request.
    /// </summary>
    [JsonPropertyName("dsTransactionId")]
    public string? DsTransactionId { get; set; }

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
