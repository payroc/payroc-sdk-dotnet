using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains surcharge information. Our gateway returns this object only if the merchant adds a surcharge to transactions.
/// </summary>
[Serializable]
public record Surcharging
{
    /// <summary>
    /// Indicates if the merchant can add a surcharge when the customer uses this card.
    /// </summary>
    [JsonPropertyName("allowed")]
    public required bool Allowed { get; set; }

    /// <summary>
    /// Surcharge amount to add to the transaction.
    /// **Note:** Our gateway returns the surcharge amount only if you include a transaction amount in the request.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// Surcharge rate that the merchant configures on their account.
    /// </summary>
    [JsonPropertyName("percentage")]
    public double? Percentage { get; set; }

    /// <summary>
    /// Statement that informs the customer about the surcharge fee.
    /// </summary>
    [JsonPropertyName("disclosure")]
    public string? Disclosure { get; set; }

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
