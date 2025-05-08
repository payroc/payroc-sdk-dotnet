using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the authorization.
/// </summary>
public record AuthorizationSummary
{
    /// <summary>
    /// Unique identifier of the authorization.
    /// </summary>
    [JsonPropertyName("authorizationId")]
    public int? AuthorizationId { get; set; }

    /// <summary>
    /// Authorization code.
    ///
    /// **Note:** For returns, the card brands may not provide an authorization code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Authorization amount. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }

    /// <summary>
    /// Response code that indicates if the address matches the address registered to the customer.
    /// </summary>
    [JsonPropertyName("avsResponseCode")]
    public string? AvsResponseCode { get; set; }

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
