using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment details for the customer’s preauthorized electronic debit (PAD) transactions.
/// </summary>
[Serializable]
public record PadPayload
{
    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** Credit card transactions don't require **accountType**.
    /// </summary>
    [JsonPropertyName("accountType")]
    public PadPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Customer's name.
    /// </summary>
    [JsonPropertyName("nameOnAccount")]
    public required string NameOnAccount { get; set; }

    /// <summary>
    /// Customer’s account number.
    /// **Note:** In responses, our gateway shows only the last four digits of the account number. For example, `*****5929`.
    /// </summary>
    [JsonPropertyName("accountNumber")]
    public required string AccountNumber { get; set; }

    /// <summary>
    /// Five-digit code that represents the customer’s bank branch.
    /// </summary>
    [JsonPropertyName("transitNumber")]
    public required string TransitNumber { get; set; }

    /// <summary>
    /// Three-digit code that represents the customer’s bank.
    /// </summary>
    [JsonPropertyName("institutionNumber")]
    public required string InstitutionNumber { get; set; }

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
