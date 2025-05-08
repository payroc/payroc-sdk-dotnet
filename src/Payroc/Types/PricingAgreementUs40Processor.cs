using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about U.S. processor fees.
/// </summary>
public record PricingAgreementUs40Processor
{
    /// <summary>
    /// Object that contains information about card fees.
    /// </summary>
    [JsonPropertyName("card")]
    public PricingAgreementUs40ProcessorCard? Card { get; set; }

    [JsonPropertyName("ach")]
    public Ach? Ach { get; set; }

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
