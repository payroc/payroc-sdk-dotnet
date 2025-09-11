using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the transaction response details.
/// </summary>
[Serializable]
public record TransactionResult : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Transaction type.
    /// </summary>
    [JsonPropertyName("type")]
    public TransactionResultType? Type { get; set; }

    /// <summary>
    /// Indicates the subtype of EBT in the transaction.
    /// </summary>
    [JsonPropertyName("ebtType")]
    public TransactionResultEbtType? EbtType { get; set; }

    /// <summary>
    /// Current status of the transaction.
    /// </summary>
    [JsonPropertyName("status")]
    public required TransactionResultStatus Status { get; set; }

    /// <summary>
    /// Authorization code that the processor assigned to the transaction.
    /// </summary>
    [JsonPropertyName("approvalCode")]
    public string? ApprovalCode { get; set; }

    /// <summary>
    /// Amount that the processor authorized for the transaction. This value is in the currency’s lowest denomination, for example, cents.
    ///
    /// **Notes:**
    /// - For partial authorizations, this amount is lower than the amount in the request.
    /// - If the value for **authorizedAmount** is negative, this indicates that the merchant sent funds to the customer.
    /// </summary>
    [JsonPropertyName("authorizedAmount")]
    public long? AuthorizedAmount { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

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
    public required TransactionResultResponseCode ResponseCode { get; set; }

    /// <summary>
    /// Response description from the processor.
    /// </summary>
    [JsonPropertyName("responseMessage")]
    public string? ResponseMessage { get; set; }

    /// <summary>
    /// Original response code that the processor sent.
    /// </summary>
    [JsonPropertyName("processorResponseCode")]
    public string? ProcessorResponseCode { get; set; }

    /// <summary>
    /// Identifier that the card brand assigns to the payment instruction.
    /// </summary>
    [JsonPropertyName("cardSchemeReferenceId")]
    public string? CardSchemeReferenceId { get; set; }

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
