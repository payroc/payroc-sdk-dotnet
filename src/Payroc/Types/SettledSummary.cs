using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the settlement.
/// </summary>
public record SettledSummary
{
    /// <summary>
    /// Processor that settled the transaction.
    /// </summary>
    [JsonPropertyName("settledBy")]
    public string? SettledBy { get; set; }

    /// <summary>
    /// Date that the processor settled the transaction. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("achDate")]
    public DateOnly? AchDate { get; set; }

    /// <summary>
    /// Unique identifier of the ACH deposit.
    /// </summary>
    [JsonPropertyName("achDepositId")]
    public int? AchDepositId { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
