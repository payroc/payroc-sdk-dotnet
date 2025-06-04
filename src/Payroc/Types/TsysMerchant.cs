using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the configuration settings for the merchant.
/// </summary>
public record TsysMerchant
{
    /// <summary>
    /// Unique identifier that the host processor assigned to the merchant.
    /// </summary>
    [JsonPropertyName("posMid")]
    public required string PosMid { get; set; }

    /// <summary>
    /// Number that represents the merchant's chain of locations or stores.
    /// </summary>
    [JsonPropertyName("chainNumber")]
    public required string ChainNumber { get; set; }

    /// <summary>
    /// Unique identifier of the merchant's settlement agent.
    /// </summary>
    [JsonPropertyName("settlementAgent")]
    public string? SettlementAgent { get; set; }

    /// <summary>
    /// Number that identifies the merchant in direct debit requests.
    /// </summary>
    [JsonPropertyName("abaNumber")]
    public string? AbaNumber { get; set; }

    /// <summary>
    /// Unique identifier of the merchant's bank.
    /// </summary>
    [JsonPropertyName("binNumber")]
    public required string BinNumber { get; set; }

    /// <summary>
    /// Number of the merchant's bank if it processes transactions on behalf of another entity.
    /// </summary>
    [JsonPropertyName("agentBankNumber")]
    public string? AgentBankNumber { get; set; }

    /// <summary>
    /// Indicates if the merchant can accept interlink debit cards.
    /// </summary>
    [JsonPropertyName("reimbursementAttribute")]
    public string? ReimbursementAttribute { get; set; }

    /// <summary>
    /// Location of the merchant's information.
    /// </summary>
    [JsonPropertyName("locationNumber")]
    public string? LocationNumber { get; set; }

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
