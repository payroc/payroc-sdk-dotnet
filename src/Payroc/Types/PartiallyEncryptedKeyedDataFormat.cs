using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the partially-encrypted card data for keyed transactions.
/// </summary>
public record PartiallyEncryptedKeyedDataFormat
{
    [JsonPropertyName("device")]
    public required EncryptionCapableDevice Device { get; set; }

    /// <summary>
    /// Encrypted card number.
    /// </summary>
    [JsonPropertyName("encryptedPan")]
    public required string EncryptedPan { get; set; }

    /// <summary>
    /// Masked card number.
    /// The gateway shows only the first six digits and the last four digits of the account number. For example, `453985******7062`.
    /// </summary>
    [JsonPropertyName("maskedPan")]
    public required string MaskedPan { get; set; }

    /// <summary>
    /// Expiry date of the customer’s card.
    /// </summary>
    [JsonPropertyName("expiryDate")]
    public required string ExpiryDate { get; set; }

    /// <summary>
    /// Security code of the customer’s card.
    /// </summary>
    [JsonPropertyName("cvv")]
    public string? Cvv { get; set; }

    /// <summary>
    /// Encrypted security code data.
    /// </summary>
    [JsonPropertyName("cvvEncrypted")]
    public string? CvvEncrypted { get; set; }

    /// <summary>
    /// Issue number of the customer’s card.
    /// </summary>
    [JsonPropertyName("issueNumber")]
    public string? IssueNumber { get; set; }

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
