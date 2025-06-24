using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains details about payment links.
/// </summary>
[Serializable]
public record ProcessingTerminalFeaturesPaymentLinks
{
    /// <summary>
    /// Indicates if the terminal supports payment links.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// URL of the logo image that the merchant wants to display in their payment link email.
    /// </summary>
    [JsonPropertyName("logoUrl")]
    public string? LogoUrl { get; set; }

    /// <summary>
    /// String that the merchant wants to display on the footer of their payment link email.
    /// </summary>
    [JsonPropertyName("footerNotes")]
    public string? FooterNotes { get; set; }

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
