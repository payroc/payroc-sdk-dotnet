using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the encrypted swiped card data.
/// </summary>
public record EncryptedSwipedDataFormat
{
    [JsonPropertyName("device")]
    public required EncryptionCapableDevice Device { get; set; }

    /// <summary>
    /// Encrypted data received from the magnetic stripe reader.
    /// </summary>
    [JsonPropertyName("encryptedData")]
    public required string EncryptedData { get; set; }

    /// <summary>
    /// First digit of the of the card number.
    /// </summary>
    [JsonPropertyName("firstDigitOfPan")]
    public string? FirstDigitOfPan { get; set; }

    /// <summary>
    /// Indicates a technical issue with the ICC transaction that resulted in the terminal falling back to a magnetic stripe transaction.
    /// </summary>
    [JsonPropertyName("fallback")]
    public bool? Fallback { get; set; }

    /// <summary>
    /// Explains the reason for the fallback.
    /// </summary>
    [JsonPropertyName("fallbackReason")]
    public EncryptedSwipedDataFormatFallbackReason? FallbackReason { get; set; }

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
