using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Array of activityRecord objects.
/// </summary>
public record ActivityRecord
{
    /// <summary>
    /// Unique identifier of the activity record.
    /// </summary>
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Date of the transaction.
    /// </summary>
    [JsonPropertyName("date")]
    public required string Date { get; set; }

    /// <summary>
    /// Name of the merchant that the activity applies to.
    /// </summary>
    [JsonPropertyName("merchant")]
    public required string Merchant { get; set; }

    /// <summary>
    /// Recipient of the debit.
    /// </summary>
    [JsonPropertyName("recipient")]
    public string? Recipient { get; set; }

    /// <summary>
    /// Description of the activity.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Total fund amount of the transaction. This is returned in the lowest unit of currency.
    /// </summary>
    [JsonPropertyName("amount")]
    public required double Amount { get; set; }

    /// <summary>
    /// Payment type.
    /// </summary>
    [JsonPropertyName("type")]
    public required ActivityRecordType Type { get; set; }

    /// <summary>
    /// Currency of all values.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

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
