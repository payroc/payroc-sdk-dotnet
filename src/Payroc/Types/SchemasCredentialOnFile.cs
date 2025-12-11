using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about saving the customer’s payment details.
/// </summary>
[Serializable]
public record SchemasCredentialOnFile : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the merchant uses a third-party vault to store the customer’s payment details.
    /// </summary>
    [JsonPropertyName("externalVault")]
    public bool? ExternalVault { get; set; }

    /// <summary>
    /// Indicates if our gateway should tokenize the customer’s payment details as part of the transaction.
    /// </summary>
    [JsonPropertyName("tokenize")]
    public bool? Tokenize { get; set; }

    /// <summary>
    /// Unique identifier that the merchant creates for the secure token that represents the customer’s payment details.
    /// **Note:** If you do not send a value for the **secureTokenId**, our gateway generates a unique identifier for the token.
    /// </summary>
    [JsonPropertyName("secureTokenId")]
    public string? SecureTokenId { get; set; }

    /// <summary>
    /// Indicates how the merchant can use the customer’s card details, as agreed by the customer:
    ///
    /// - `unscheduled` - Transactions for a fixed or variable amount that are run at a certain pre-defined event.
    /// - `recurring` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Recurring transactions don’t have a fixed duration and run until the customer cancels the agreement.
    /// - `installment` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Installment transactions have a fixed duration.
    ///
    /// **Note:** If you send a value for **mitAgreement**, you must send the **standingInstructions** object in the **paymentOrder** object.
    /// </summary>
    [JsonPropertyName("mitAgreement")]
    public SchemasCredentialOnFileMitAgreement? MitAgreement { get; set; }

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
