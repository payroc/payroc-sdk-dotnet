using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the owner's relationship to the business.
/// </summary>
[Serializable]
public record OwnerRelationship
{
    /// <summary>
    /// Percentage equity stake that the owner holds in the business.
    /// </summary>
    [JsonPropertyName("equityPercentage")]
    public float? EquityPercentage { get; set; }

    /// <summary>
    /// Owner's job title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Indicates if the owner is a control prong. You can identify only one control prong for a business.
    /// </summary>
    [JsonPropertyName("isControlProng")]
    public required bool IsControlProng { get; set; }

    /// <summary>
    /// Indicates if the owner is an authorized signatory.
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
