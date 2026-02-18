using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record ListAuthorizationsSettlementResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Array of authorization objects.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<Authorization> Data { get; set; } = new List<Authorization>();

    /// <summary>
    /// Maximum number of results that we return for each page.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Number of results we returned on this page.
    ///
    /// **Note:** This might not be the total number of results that match your query.
    /// </summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    /// <summary>
    /// Indicates whether there is another page of results available.
    /// </summary>
    [JsonPropertyName("hasMore")]
    public bool? HasMore { get; set; }

    /// <summary>
    /// Reference links to navigate to the previous page of results or to the next page of results.
    /// </summary>
    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

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
