using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment details in the customer’s digital wallet.
/// </summary>
public record DigitalWalletPayload
{
    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** Credit card transactions don't require **accountType**.
    /// </summary>
    [JsonPropertyName("accountType")]
    public DigitalWalletPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Provider of the digital wallet.
    /// </summary>
    [JsonPropertyName("serviceProvider")]
    public required DigitalWalletPayloadServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Cardholder’s name.
    /// </summary>
    [JsonPropertyName("cardholderName")]
    public string? CardholderName { get; set; }

    /// <summary>
    /// Encrypted data of the digital wallet.
    /// </summary>
    [JsonPropertyName("encryptedData")]
    public required string EncryptedData { get; set; }

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
