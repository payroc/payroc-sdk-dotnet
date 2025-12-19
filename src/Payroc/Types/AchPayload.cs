using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment details for the customer’s automated clearing house (ACH) transactions.
/// </summary>
[Serializable]
public record AchPayload : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the customer’s account type.
    ///
    /// **Note:** For bank account details, send a value for accountType.
    /// </summary>
    [JsonPropertyName("accountType")]
    public AchPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Indicates how the customer authorized the ACH transaction. Send one of the following values:
    ///
    /// - `web` – Online transaction.
    /// - `tel` – Telephone transaction.
    /// - `ccd` – Corporate credit card or debit card transaction.
    /// - `ppd` – Pre-arranged transaction.
    /// </summary>
    [JsonPropertyName("secCode")]
    public AchPayloadSecCode? SecCode { get; set; }

    /// <summary>
    /// Customer's name.
    /// </summary>
    [JsonPropertyName("nameOnAccount")]
    public required string NameOnAccount { get; set; }

    /// <summary>
    /// Customer’s bank account number.
    /// **Note:** In responses, our gateway shows only the last four digits of the account number, for example, `*****5929`.
    /// </summary>
    [JsonPropertyName("accountNumber")]
    public required string AccountNumber { get; set; }

    /// <summary>
    /// Nine-digit number that identifies the customer's bank.
    /// </summary>
    [JsonPropertyName("routingNumber")]
    public required string RoutingNumber { get; set; }

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
