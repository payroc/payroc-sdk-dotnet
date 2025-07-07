using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the 3-D Secure information from a third party.
/// </summary>
[Serializable]
public record ThirdPartyThreeDSecure
{
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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
