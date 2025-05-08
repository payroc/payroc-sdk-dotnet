using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about a return.
/// </summary>
public record BankTransferReturnSummary
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the payment.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public required string PaymentId { get; set; }

    /// <summary>
    /// The date that the check was returned.
    /// </summary>
    [JsonPropertyName("date")]
    public required string Date { get; set; }

    /// <summary>
    /// The NACHA return code.
    /// </summary>
    [JsonPropertyName("returnCode")]
    public required string ReturnCode { get; set; }

    /// <summary>
    /// The reason why the check was returned.
    /// </summary>
    [JsonPropertyName("returnReason")]
    public required string ReturnReason { get; set; }

    /// <summary>
    /// Indicates whether the return has been re-presented.
    /// </summary>
    [JsonPropertyName("represented")]
    public required bool Represented { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
