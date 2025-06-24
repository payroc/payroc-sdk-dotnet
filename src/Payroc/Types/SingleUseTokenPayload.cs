using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the single-use token, which represents the customer’s payment details.
/// </summary>
[Serializable]
public record SingleUseTokenPayload
{
    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** Credit card transactions don't require **accountType**.
    /// </summary>
    [JsonPropertyName("accountType")]
    public SingleUseTokenPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Unique token that the gateway assigned to the payment details.
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    [JsonPropertyName("pinDetails")]
    public SingleUseTokenPayloadPinDetails? PinDetails { get; set; }

    [JsonPropertyName("ebtDetails")]
    public EbtDetailsWithVoucher? EbtDetails { get; set; }

    /// <summary>
    /// Indicates the type of authorization for the transaction. The field is mandatory for ACH secure token.
    /// - `web` – Online transaction.
    /// - `tel` – Telephone transaction.
    /// - `ccd` – Corporate credit card or debit card transaction.
    /// - `ppd` – Pre-arranged transaction.
    /// </summary>
    [JsonPropertyName("secCode")]
    public SingleUseTokenPayloadSecCode? SecCode { get; set; }

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
