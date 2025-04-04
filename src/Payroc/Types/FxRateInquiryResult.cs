using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the currency conversion rate.
/// </summary>
public record FxRateInquiryResult
{
    /// <summary>
    /// Indicates if the card is eligible for DCC.
    /// </summary>
    [JsonPropertyName("dccOffered")]
    public required bool DccOffered { get; set; }

    /// <summary>
    /// Explains why the DCC service did not offer a currency conversion rate to the customer.
    /// </summary>
    [JsonPropertyName("causeOfRejection")]
    public string? CauseOfRejection { get; set; }

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
