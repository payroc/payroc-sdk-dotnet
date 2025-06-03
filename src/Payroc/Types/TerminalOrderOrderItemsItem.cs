using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record TerminalOrderOrderItemsItem
{
    /// <summary>
    /// Type of item.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "solution";

    /// <summary>
    /// Unique identifier of the solution. Send one of the following values:
    /// - `Roc Services_DX8000`
    /// - `Roc Services_DX4000`
    /// - `Roc Services_Web`
    /// - `Roc Services_Mobile`
    /// - `Payroc DX8000`
    /// - `Payroc DX4000`
    /// - `Payroc RX7000_Cloud`
    /// - `Payroc DX8000_Cloud`
    /// - `Payroc DX4000_Cloud`
    /// - `Payroc A920Pro`
    /// - `Payroc A80`
    /// - `Payroc A920Pro_Cloud`
    /// - `Payroc A80_Cloud`
    /// - `Roc Terminal Plus_N950`
    /// - `Roc Terminal Plus_N950-S`
    /// - `Roc Terminal Plus_X800`
    /// - `Gateway_Payroc`
    /// - `VAR_Only_TSYS`
    /// - `ROC Services Chipper3X`
    /// - `BBPOS Chipper 3X`
    /// - `Augusta EMV`
    /// - `Ingenico - AXIUM Full Functional Base`
    /// - `Pax A920 Charging Base`
    /// - `Pax A920 Comms Base`
    /// </summary>
    [JsonPropertyName("solutionTemplateId")]
    public required string SolutionTemplateId { get; set; }

    /// <summary>
    /// Quantity of the solution.
    /// </summary>
    [JsonPropertyName("solutionQuantity")]
    public float? SolutionQuantity { get; set; }

    /// <summary>
    /// Indicates if the order contains a new item or a refurbished item.
    /// </summary>
    [JsonPropertyName("deviceCondition")]
    public OrderItemDeviceCondition? DeviceCondition { get; set; }

    /// <summary>
    /// Object that contains the settings for the solution, including gateway settings, device settings, and application settings.
    /// </summary>
    [JsonPropertyName("solutionSetup")]
    public OrderItemSolutionSetup? SolutionSetup { get; set; }

    [JsonPropertyName("links")]
    public IEnumerable<ProcessingTerminalSummary>? Links { get; set; }

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
