using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record CardVerificationResult
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who requested to verify the card.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Indicates if we have verified the card details.
    /// - `true` - The card details are valid.
    /// - `false` - The card details are not valid.
    /// </summary>
    [JsonPropertyName("verified")]
    public required bool Verified { get; set; }

    /// <summary>
    /// Date and time that we processed the request. This format follows the ISO 8601 standard, for example, 2024-07-02T15:02:07+00:00.
    /// </summary>
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }

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
    public CardVerificationResultResponseCode? ResponseCode { get; set; }

    /// <summary>
    /// Response description from the payment processor. For example, "Refer to Card Issuer".
    /// </summary>
    [JsonPropertyName("responseMessage")]
    public string? ResponseMessage { get; set; }

    /// <summary>
    /// Response code from payment processor. This code is then mapped onto a `responseCode` enum.
    /// </summary>
    [JsonPropertyName("processorResponseCode")]
    public string? ProcessorResponseCode { get; set; }

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
