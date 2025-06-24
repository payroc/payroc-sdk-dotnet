using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ProcessingAchRefunds
{
    /// <summary>
    /// Indicates if the business has a written refund policy.
    /// </summary>
    [JsonPropertyName("writtenRefundPolicy")]
    public required bool WrittenRefundPolicy { get; set; }

    /// <summary>
    /// URL of the written refund policy.
    /// </summary>
    [JsonPropertyName("refundPolicyUrl")]
    public string? RefundPolicyUrl { get; set; }

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
