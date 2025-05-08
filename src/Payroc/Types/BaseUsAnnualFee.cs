using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the annual fee.
/// </summary>
public record BaseUsAnnualFee
{
    /// <summary>
    /// Indicates the month in which we collect the annual fee.
    /// </summary>
    [JsonPropertyName("billInMonth")]
    public BaseUsAnnualFeeBillInMonth? BillInMonth { get; set; }

    /// <summary>
    /// Annual fee. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required int Amount { get; set; }

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
