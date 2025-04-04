using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Flat Rate fees.
/// </summary>
public record FlatRateFees
{
    [JsonPropertyName("standardCards")]
    public required ProcessorFee StandardCards { get; set; }

    [JsonPropertyName("amex")]
    public FlatRateFeesAmex? Amex { get; set; }

    [JsonPropertyName("pinDebit")]
    public PinDebit? PinDebit { get; set; }

    [JsonPropertyName("electronicBenefitsTransfer")]
    public ElectronicBenefitsTransfer? ElectronicBenefitsTransfer { get; set; }

    [JsonPropertyName("specialityCards")]
    public SpecialityCards? SpecialityCards { get; set; }

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
