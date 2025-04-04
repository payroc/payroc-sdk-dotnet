using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public record ListTransactionsSettlementResponse
{
    /// <summary>
    /// Array of transaction objects.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<Transaction> Data { get; set; } = new List<Transaction>();

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
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
