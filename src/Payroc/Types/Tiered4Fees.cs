using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the fees.
/// </summary>
public record Tiered4Fees
{
    [JsonPropertyName("mastercardVisaDiscover")]
    public required QualRatesWithPremium MastercardVisaDiscover { get; set; }

    [JsonPropertyName("amex")]
    public Tiered4FeesAmex? Amex { get; set; }

    [JsonPropertyName("pinDebit")]
    public PinDebit? PinDebit { get; set; }

    [JsonPropertyName("electronicBenefitsTransfer")]
    public ElectronicBenefitsTransfer? ElectronicBenefitsTransfer { get; set; }

    [JsonPropertyName("specialityCards")]
    public SpecialityCards? SpecialityCards { get; set; }

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
