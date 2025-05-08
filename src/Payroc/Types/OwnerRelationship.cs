using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the owner's relationship to the business.
/// </summary>
public record OwnerRelationship
{
    /// <summary>
    /// Percentage of the business that the owner holds.
    /// </summary>
    [JsonPropertyName("equityPercentage")]
    public float? EquityPercentage { get; set; }

    /// <summary>
    /// Owner's job title within the business.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Indicates if the owner is a control prong within the business. Only one control prong should be identified.
    /// </summary>
    [JsonPropertyName("isControlProng")]
    public required bool IsControlProng { get; set; }

    /// <summary>
    /// Indicates if the owner is an authorized signatory within the business.
    /// </summary>
    [JsonPropertyName("isAuthorizedSignatory")]
    public bool? IsAuthorizedSignatory { get; set; }

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
