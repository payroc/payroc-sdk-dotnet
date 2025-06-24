using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

[Serializable]
public record ListFundingInstructionsResponse
{
    /// <summary>
    /// Array of fundingInstruction objects.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<ListFundingInstructionsResponseDataItem> Data { get; set; } =
        new List<ListFundingInstructionsResponseDataItem>();

    /// <summary>
    /// Maximum number of results that we return for each page.
    /// </summary>
    [JsonPropertyName("limit")]
    public double? Limit { get; set; }

    /// <summary>
    /// Number of results that we returned.
    /// </summary>
    [JsonPropertyName("count")]
    public double? Count { get; set; }

    /// <summary>
    /// Indicates that further results are available.
    /// </summary>
    [JsonPropertyName("hasMore")]
    public bool? HasMore { get; set; }

    /// <summary>
    /// Reference links to navigate to the previous page of results, or to the next page of results.
    /// </summary>
    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

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
