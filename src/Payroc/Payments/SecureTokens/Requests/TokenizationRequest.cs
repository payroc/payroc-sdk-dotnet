using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.SecureTokens;

public record TokenizationRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that the merchant created for the secure token that represents the customer's payment details.
    /// If the merchant doesn't create a secureTokenId, the gateway generates one and returns it in the response.
    /// </summary>
    [JsonPropertyName("secureTokenId")]
    public string? SecureTokenId { get; set; }

    /// <summary>
    /// Operator who saved the customer's payment details.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Indicates how the merchant can use the customer's card details, as agreed by the customer:
    ///
    /// - `unscheduled` - Transactions for a fixed or variable amount that are run at a certain pre-defined event.
    /// - `recurring` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Recurring transactions don't have a fixed duration and run until the customer cancels the agreement.
    /// - `installment` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Installment transactions have a fixed duration.
    /// </summary>
    [JsonPropertyName("mitAgreement")]
    public TokenizationRequestMitAgreement? MitAgreement { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("ipAddress")]
    public IpAddress? IpAddress { get; set; }

    /// <summary>
    /// Object that contains information about the payment method to tokenize.
    /// </summary>
    [JsonPropertyName("source")]
    public required TokenizationRequestSource Source { get; set; }

    /// <summary>
    /// Object that contains information for an authentication check on the customer's payment details using the 3-D Secure protocol.
    /// </summary>
    [JsonPropertyName("threeDSecure")]
    public TokenizationRequestThreeDSecure? ThreeDSecure { get; set; }

    /// <summary>
    /// Array of customField objects.
    /// </summary>
    [JsonPropertyName("customFields")]
    public IEnumerable<CustomField>? CustomFields { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
