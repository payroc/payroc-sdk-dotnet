using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the secure token.
/// </summary>
[Serializable]
public record SecureTokenSummary
{
    /// <summary>
    /// Unique identifier that the merchant assigned to the secure token.
    /// </summary>
    [JsonPropertyName("secureTokenId")]
    public required string SecureTokenId { get; set; }

    /// <summary>
    /// Customer's name.
    /// </summary>
    [JsonPropertyName("customerName")]
    public required string CustomerName { get; set; }

    /// <summary>
    /// Token that the merchant can use in future transactions to represent the customer's payment details. The token:
    /// - Begins with the six-digit identification number **296753**.
    /// - Contains up to 12 digits.
    /// - Contains a single check digit that we calculate using the Luhn algorithm.
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    /// <summary>
    /// Status of the customer's bank account. The processor performs a security check on the customer's bank account and returns the status of the account.
    /// **Note:** Depending on the merchant's account settings, this feature may be unavailable.
    /// </summary>
    [JsonPropertyName("status")]
    public required SecureTokenSummaryStatus Status { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
