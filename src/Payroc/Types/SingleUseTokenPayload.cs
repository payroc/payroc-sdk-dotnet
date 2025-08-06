using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the single-use token, which represents the customer’s payment details.
/// </summary>
[Serializable]
public record SingleUseTokenPayload : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the customer’s account type.
    ///
    /// **Note:** Send a value for accountType only if the single-use token represents bank account details.
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
    /// Indicates how the customer authorized the ACH transaction. Send one of the following values:
    ///
    /// - `web` – Online transaction.
    /// - `tel` – Telephone transaction.
    /// - `ccd` – Corporate credit card or debit card transaction.
    /// - `ppd` – Pre-arranged transaction.
    /// </summary>
    [JsonPropertyName("secCode")]
    public SingleUseTokenPayloadSecCode? SecCode { get; set; }

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
