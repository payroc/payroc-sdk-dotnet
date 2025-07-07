using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about a payment.
/// </summary>
[Serializable]
public record PaymentSummary
{
    /// <summary>
    /// Unique identifier of the payment.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Date and time that the payment was processed.
    /// </summary>
    [JsonPropertyName("dateTime")]
    public required DateTime DateTime { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    /// <summary>
    /// Amount of the payment. This value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    /// <summary>
    /// Current status of the payment.
    /// </summary>
    [JsonPropertyName("status")]
    public required PaymentSummaryStatus Status { get; set; }

    /// <summary>
    /// Response from the processor.
    /// - `A` - The processor approved the transaction.
    /// - `D` - The processor declined the transaction.
    /// - `E` - The processor received the transaction but will process the transaction later.
    /// - `P` - The processor authorized a portion of the original amount of the transaction.
    /// - `R` - The issuer declined the transaction and indicated that the customer should contact their bank.
    /// - `C` - The issuer declined the transaction and indicated that the merchant should keep the card as it was reported lost or stolen.
    /// </summary>
    [JsonPropertyName("responseCode")]
    public required PaymentSummaryResponseCode ResponseCode { get; set; }

    /// <summary>
    /// Response description from the processor.
    /// </summary>
    [JsonPropertyName("responseMessage")]
    public required string ResponseMessage { get; set; }

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
