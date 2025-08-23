using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that indicates if the customer's card is eligible for Dynamic Currency Conversion (DCC).
/// </summary>
[Serializable]
public record FxRateInquiryResult : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the card is eligible for Dynamic Currency Conversion (DCC).
    /// </summary>
    [JsonPropertyName("dccOffered")]
    public required bool DccOffered { get; set; }

    /// <summary>
    /// Explains why the DCC service did not offer a currency conversion rate to the customer.
    /// </summary>
    [JsonPropertyName("causeOfRejection")]
    public string? CauseOfRejection { get; set; }

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
