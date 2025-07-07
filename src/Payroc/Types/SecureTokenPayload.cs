using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the secure token that represents the customer’s payment details.
/// </summary>
[Serializable]
public record SecureTokenPayload
{
    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** Credit card transactions don't require **accountType**.
    /// </summary>
    [JsonPropertyName("accountType")]
    public SecureTokenPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Unique token that the gateway assigned to the payment details.
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    /// <summary>
    /// Indicates how the customer authorized the ACH transaction. Send one of the following values:
    ///
    /// - `web` – Online transaction.
    /// - `tel` – Telephone transaction.
    /// - `ccd` – Corporate credit card or debit card transaction.
    /// - `ppd` – Pre-arranged transaction.
    /// </summary>
    [JsonPropertyName("secCode")]
    public SecureTokenPayloadSecCode? SecCode { get; set; }

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
