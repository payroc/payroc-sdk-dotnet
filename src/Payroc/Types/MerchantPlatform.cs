using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record MerchantPlatform
{
    /// <summary>
    /// Unique identifier of the merchant platform.
    /// </summary>
    [JsonPropertyName("merchantPlatformId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public string? MerchantPlatformId { get; set; }

    /// <summary>
    /// Date that the merchant platform was created.
    /// </summary>
    [JsonPropertyName("createdDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date that the merchant platform was last modified.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? LastModifiedDate { get; set; }

    [JsonPropertyName("business")]
    public required Business Business { get; set; }

    /// <summary>
    /// Array of processingAccount objects
    /// </summary>
    [JsonPropertyName("processingAccounts")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public IEnumerable<MerchantPlatformProcessingAccountsItem> ProcessingAccounts { get; set; } =
        new List<MerchantPlatformProcessingAccountsItem>();

    /// <summary>
    /// Object that you can send to include custom metadata in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of useful links related to your request
    /// </summary>
    [JsonPropertyName("links")]
    [JsonAccess(JsonAccessType.ReadOnly)]
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
