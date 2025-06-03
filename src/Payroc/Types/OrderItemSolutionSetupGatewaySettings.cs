using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the gateway settings for the solution.
/// </summary>
public record OrderItemSolutionSetupGatewaySettings
{
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
