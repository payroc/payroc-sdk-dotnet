using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains details about EBT transactions.
/// </summary>
public record EbtEnabled
{
    /// <summary>
    /// Indicates if the terminal accepts Electronic Benefit Transfer (EBT) transactions.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Indicates the type of EBT that the terminal supports.
    /// </summary>
    [JsonPropertyName("ebtType")]
    public required EbtEnabledEbtType EbtType { get; set; }

    /// <summary>
    /// Food and Nutritional Service (FNS) number that the government assigns to the merchant to allow them to accept Supplemental Nutrition Assistance Program (SNAP) transactions.
    /// </summary>
    [JsonPropertyName("fnsNumber")]
    public string? FnsNumber { get; set; }

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
