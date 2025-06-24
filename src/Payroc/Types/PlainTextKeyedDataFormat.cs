using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the plain-text card data for keyed transactions.
/// </summary>
[Serializable]
public record PlainTextKeyedDataFormat
{
    [JsonPropertyName("device")]
    public Device? Device { get; set; }

    /// <summary>
    /// Customer’s card number.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public required string CardNumber { get; set; }

    /// <summary>
    /// Expiry date of the customer’s card.
    /// **Note:** This field is required for most BIN lookups or electronic voucher transactions.
    /// </summary>
    [JsonPropertyName("expiryDate")]
    public string? ExpiryDate { get; set; }

    /// <summary>
    /// Security code of the customer’s card.
    /// </summary>
    [JsonPropertyName("cvv")]
    public string? Cvv { get; set; }

    /// <summary>
    /// Issue number of the customer’s card.
    /// </summary>
    [JsonPropertyName("issueNumber")]
    public string? IssueNumber { get; set; }

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
