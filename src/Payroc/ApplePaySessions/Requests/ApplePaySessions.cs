using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.ApplePaySessions;

[Serializable]
public record ApplePaySessions
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique appleDomainId of the merchant's domain that we assigned when you added their domain to our Self-Care Portal.
    /// </summary>
    [JsonPropertyName("appleDomainId")]
    public required string AppleDomainId { get; set; }

    /// <summary>
    /// Validation URL from the Apple Pay JS API.
    /// </summary>
    [JsonPropertyName("appleValidationUrl")]
    public required string AppleValidationUrl { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
