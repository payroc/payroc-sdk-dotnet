using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the authorization.
/// </summary>
[Serializable]
public record Authorization
{
    /// <summary>
    /// Unique identifier of the authorization.
    /// </summary>
    [JsonPropertyName("authorizationId")]
    public int? AuthorizationId { get; set; }

    /// <summary>
    /// Date that we received the authorization. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public DateOnly? CreatedDate { get; set; }

    /// <summary>
    /// Date that the authorization was last changed. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public DateOnly? LastModifiedDate { get; set; }

    /// <summary>
    /// Code that indicates the response for the authorization.
    /// </summary>
    [JsonPropertyName("authorizationResponse")]
    public AuthorizationAuthorizationResponse? AuthorizationResponse { get; set; }

    /// <summary>
    /// Amount that the merchant requested for the authorization. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("preauthorizationRequestAmount")]
    public int? PreauthorizationRequestAmount { get; set; }

    /// <summary>
    /// Currency of the authorization.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("batch")]
    public BatchSummary? Batch { get; set; }

    [JsonPropertyName("card")]
    public CardSummary? Card { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantSummary? Merchant { get; set; }

    [JsonPropertyName("transaction")]
    public TransactionSummary? Transaction { get; set; }

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
