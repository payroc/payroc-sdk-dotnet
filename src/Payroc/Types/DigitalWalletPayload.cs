using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment details in the customer’s digital wallet.
/// </summary>
[Serializable]
public record DigitalWalletPayload
{
    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** Credit card transactions don't require **accountType**.
    /// </summary>
    [JsonPropertyName("accountType")]
    public DigitalWalletPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Provider of the digital wallet. Send one of the following values:
    /// - `apple` - For more information about how to integrate with Apple Pay, go to [Apple Pay®](/guides/integrate/apple-pay).
    /// - `google` - For more information about how to integrate with google Pay, go to [Google Pay®](/guides/integrate/google-pay).
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
