using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record FundingRecipientOwnersItem
{
    /// <summary>
    /// Unique identifier of the owner.
    /// </summary>
    [JsonPropertyName("ownerId")]
    public int? OwnerId { get; set; }

    /// <summary>
    /// Array of HATEOAS links for viewing the owner.
    /// </summary>
    [JsonPropertyName("link")]
    public FundingRecipientOwnersItemLink? Link { get; set; }

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
