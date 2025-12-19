using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Array of activityRecord objects.
/// </summary>
[Serializable]
public record ActivityRecord : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the activity.
    /// </summary>
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Date that we moved the funds.
    /// </summary>
    [JsonPropertyName("date")]
    public required string Date { get; set; }

    /// <summary>
    /// Doing business as (DBA) name of the merchant that owns the funding balance.
    /// </summary>
    [JsonPropertyName("merchant")]
    public required string Merchant { get; set; }

    /// <summary>
    /// Name of the account holder who owns the funding account that received funds.
    ///
    /// **Note:** We return a value for recipient only if the value for type is `debit`.
    /// </summary>
    [JsonPropertyName("recipient")]
    public string? Recipient { get; set; }

    /// <summary>
    /// Description of the activity.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Total amount that we removed or added to the merchant's funding balance. The value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required double Amount { get; set; }

    /// <summary>
    /// Indicates if we moved funds into or out of the funding balance. Our gateway returns one of the following values:
    /// -	`credit` - We moved funds into the funding balance.
    /// -	`debit` - We moved funds out of the funding balance.
    /// </summary>
    [JsonPropertyName("type")]
    public required ActivityRecordType Type { get; set; }

    /// <summary>
    /// Currency of the funds. We return a value of `USD`.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

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
