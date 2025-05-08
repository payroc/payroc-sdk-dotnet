using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Instruction indicating which recipients should receive funding from the specific merchant balance.
/// </summary>
public record InstructionMerchantsItem
{
    /// <summary>
    /// Unique identifier of the merchant.
    /// </summary>
    [JsonPropertyName("merchantId")]
    public required string MerchantId { get; set; }

    /// <summary>
    /// Array of target fundingAccount objects.
    /// </summary>
    [JsonPropertyName("recipients")]
    public IEnumerable<InstructionMerchantsItemRecipientsItem> Recipients { get; set; } =
        new List<InstructionMerchantsItemRecipientsItem>();

    /// <summary>
    /// Array of HATEOAS links to view the merchant.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("link")]
    public InstructionMerchantsItemLink? Link { get; set; }

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
