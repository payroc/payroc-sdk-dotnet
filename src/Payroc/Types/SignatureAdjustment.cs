using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the signature for the transaction.
/// **Note:** If the merchant previously added a signature to the transaction, they can’t adjust or delete the signature.
/// </summary>
[Serializable]
public record SignatureAdjustment
{
    /// <summary>
    /// Cardholder’s signature. For more information about the format of the signature, see Special Fields and Parameters.
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public required string CardholderSignature { get; set; }

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
