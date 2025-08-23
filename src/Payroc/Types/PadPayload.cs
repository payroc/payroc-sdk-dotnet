using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment details for the customer’s preauthorized electronic debit (PAD) transactions.
/// </summary>
[Serializable]
public record PadPayload : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** For bank account details, send a value for accountType.
    /// </summary>
    [JsonPropertyName("accountType")]
    public PadPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Customer's name.
    /// </summary>
    [JsonPropertyName("nameOnAccount")]
    public required string NameOnAccount { get; set; }

    /// <summary>
    /// Customer's account number.
    /// **Note:** In responses, our gateway shows only the last four digits of the account number, for example, `*****5929`.
    /// </summary>
    [JsonPropertyName("accountNumber")]
    public required string AccountNumber { get; set; }

    /// <summary>
    /// Five-digit number that identifies the customer's bank branch.
    /// </summary>
    [JsonPropertyName("transitNumber")]
    public required string TransitNumber { get; set; }

    /// <summary>
    /// Three-digit number that identifies the customer's bank.
    /// </summary>
    [JsonPropertyName("institutionNumber")]
    public required string InstitutionNumber { get; set; }

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
