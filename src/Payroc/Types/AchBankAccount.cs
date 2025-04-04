using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the customer's account details.
/// </summary>
public record AchBankAccount
{
    /// <summary>
    /// SEC code for the transaction.
    /// - `web` - Online transaction.
    /// - `tel` - Telephone transaction.
    /// - `ccd` - Corporate credit or debit transaction.
    /// - `ppd` - Pre-arranged transaction.
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
    /// **Note:** In responses, our gateway shows only the last four digits of the account’s routing number. For example, *****4162.
    /// </summary>
    [JsonPropertyName("routingNumber")]
    public required string RoutingNumber { get; set; }

    [JsonPropertyName("secureToken")]
    public SecureTokenSummary? SecureToken { get; set; }

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
