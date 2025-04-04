using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record FundingAccountSummary
{
    [JsonPropertyName("fundingAccountId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? FundingAccountId { get; set; }

    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public FundingAccountSummaryStatus? Status { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
