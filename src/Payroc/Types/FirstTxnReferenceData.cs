using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the initial payment for the payment instruction.
/// </summary>
public record FirstTxnReferenceData
{
    /// <summary>
    /// Unique identifier of the first payment.
    /// **Note:** We recommend that you always send a value for **paymentId**.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public string? PaymentId { get; set; }

    /// <summary>
    /// Identifier that the card brand assigns to the payment instruction.
    /// </summary>
    [JsonPropertyName("cardSchemeReferenceId")]
    public string? CardSchemeReferenceId { get; set; }

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
