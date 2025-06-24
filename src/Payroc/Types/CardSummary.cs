using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the card.
/// </summary>
[Serializable]
public record CardSummary
{
    /// <summary>
    /// Card number. We mask the number except for the first six digits and the last four digits.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public string? CardNumber { get; set; }

    /// <summary>
    /// Card type. If we can't match a dispute to a transaction, we don't return a 'type' object.
    /// </summary>
    [JsonPropertyName("type")]
    public CardSummaryType? Type { get; set; }

    /// <summary>
    /// Indicates if the cardholder provided the CVV.
    /// </summary>
    [JsonPropertyName("cvvPresenceIndicator")]
    public bool? CvvPresenceIndicator { get; set; }

    /// <summary>
    /// Indicates if the AVS was used to verify the cardholder's address.
    /// </summary>
    [JsonPropertyName("avsRequest")]
    public bool? AvsRequest { get; set; }

    /// <summary>
    /// Response from the AVS.
    /// </summary>
    [JsonPropertyName("avsResponse")]
    public string? AvsResponse { get; set; }

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
