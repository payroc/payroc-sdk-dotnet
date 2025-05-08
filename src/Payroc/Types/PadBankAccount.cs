using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the customer's account details.
/// </summary>
public record PadBankAccount
{
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
    /// Five-digit code that represents the customer's banking branch.
    /// </summary>
    [JsonPropertyName("transitNumber")]
    public required string TransitNumber { get; set; }

    /// <summary>
    /// Three-digit code that represents the customer's bank.
    /// </summary>
    [JsonPropertyName("institutionNumber")]
    public required string InstitutionNumber { get; set; }

    [JsonPropertyName("secureToken")]
    public SecureTokenSummary? SecureToken { get; set; }

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
