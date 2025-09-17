using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the authorization.
/// </summary>
[Serializable]
public record Authorization : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the authorization.
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
    /// Response from the issuing bank for the authorization.
    /// </summary>
    [JsonPropertyName("authorizationResponse")]
    public AuthorizationAuthorizationResponse? AuthorizationResponse { get; set; }

    /// <summary>
    /// Amount that the merchant requested for the authorization. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("preauthorizationRequestAmount")]
    public int? PreauthorizationRequestAmount { get; set; }

    /// <summary>
    /// Currency of the authorization. The value for the currency follows the [ISO 4217](https://www.iso.org/iso-4217-currency-codes.html) standard.
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
