using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the gateway settings for the solution.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupGatewaySettings : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the merchant portfolio.
    /// </summary>
    [JsonPropertyName("merchantPortfolioId")]
    public string? MerchantPortfolioId { get; set; }

    /// <summary>
    /// Unique identifier of the gateway merchant template.
    /// </summary>
    [JsonPropertyName("merchantTemplateId")]
    public string? MerchantTemplateId { get; set; }

    /// <summary>
    /// Unique identifier of the gateway user template.
    /// </summary>
    [JsonPropertyName("userTemplateId")]
    public string? UserTemplateId { get; set; }

    /// <summary>
    /// Unique identifier of the gateway terminal template.
    /// </summary>
    [JsonPropertyName("terminalTemplateId")]
    public string? TerminalTemplateId { get; set; }

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
