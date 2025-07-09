using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the secure token.
/// </summary>
[Serializable]
public record SecureToken : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that the merchant created for the secure token that represents the customer's payment details.
    /// </summary>
    [JsonPropertyName("secureTokenId")]
    public required string SecureTokenId { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Indicates how the merchant can use the customer's card details, as agreed by the customer:
    ///
    /// - `unscheduled` - Transactions for a fixed or variable amount that are run at a certain pre-defined event.
    /// - `recurring` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Recurring transactions don't have a fixed duration and run until the customer cancels the agreement.
    /// - `installment` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Installment transactions have a fixed duration.
    /// </summary>
    [JsonPropertyName("mitAgreement")]
    public SecureTokenMitAgreement? MitAgreement { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    /// <summary>
    /// Object that contains information about the payment method that we tokenized.
    /// </summary>
    [JsonPropertyName("source")]
    public required SecureTokenSource Source { get; set; }

    /// <summary>
    /// Token that the merchant can use in future transactions to represent the customer's payment details. The token:
    /// - Begins with the six-digit identification number **296753**.
    /// - Contains up to 12 digits.
    /// - Contains a single check digit that we calculate using the Luhn algorithm.
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    /// <summary>
    /// Outcome of a security check on the status of the customer's payment card or bank account.
    ///
    /// **Note:** Depending on the merchant's account settings, this feature may be unavailable.
    /// </summary>
    [JsonPropertyName("status")]
    public required SecureTokenStatus Status { get; set; }

    /// <summary>
    /// Array of customField objects.
    /// </summary>
    [JsonPropertyName("customFields")]
    public IEnumerable<CustomField>? CustomFields { get; set; }

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
