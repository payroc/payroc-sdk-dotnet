using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the adjustment to the transaction. Send this object if the merchant is adjusting the status of the transaction.
/// </summary>
[Serializable]
public record StatusAdjustment
{
    /// <summary>
    /// Status of the transaction.
    /// </summary>
    [JsonPropertyName("toStatus")]
    public required StatusAdjustmentToStatus ToStatus { get; set; }

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
