using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the merchant.
/// </summary>
public record MerchantSummary
{
    /// <summary>
    /// Unique identifier of the merchant.
    /// </summary>
    [JsonPropertyName("merchantId")]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Legal name that the business operates as.
    /// </summary>
    [JsonPropertyName("doingBusinessAs")]
    public string? DoingBusinessAs { get; set; }

    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonPropertyName("processingAccountId")]
    public string? ProcessingAccountId { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
