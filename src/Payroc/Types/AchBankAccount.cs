using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the customer's account details.
/// </summary>
[Serializable]
public record AchBankAccount : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the type of authorization for the transaction.
    ///
    /// **Note:** The field is mandatory for ACH secure token.
    ///
    /// - `web` – Online transaction.
    /// - `tel` – Telephone transaction.
    /// - `ccd` – Corporate credit card or debit card transaction.
    /// - `ppd` – Pre-arranged transaction.
    /// </summary>
    [JsonPropertyName("secCode")]
    public AchBankAccountSecCode? SecCode { get; set; }

    /// <summary>
    /// Customer's name.
    /// </summary>
    [JsonPropertyName("nameOnAccount")]
    public required string NameOnAccount { get; set; }

    /// <summary>
    /// Customer's bank account number. We mask all digits except the last four digits.
    /// </summary>
    [JsonPropertyName("accountNumber")]
    public required string AccountNumber { get; set; }

    /// <summary>
    /// Routing number of the customer’s account.
    ///
    /// **Note:** In responses, our gateway shows only the last four digits of the account's routing number, for example, *****4162.
    /// </summary>
    [JsonPropertyName("routingNumber")]
    public required string RoutingNumber { get; set; }

    [JsonPropertyName("secureToken")]
    public SecureTokenSummary? SecureToken { get; set; }

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
