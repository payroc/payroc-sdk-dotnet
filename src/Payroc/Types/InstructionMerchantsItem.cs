using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Instruction indicating which recipients should receive funding from the specific merchant balance.
/// </summary>
[Serializable]
public record InstructionMerchantsItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
