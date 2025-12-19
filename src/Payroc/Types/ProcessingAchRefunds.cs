using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the ACH refund policy for the processing account.
/// </summary>
[Serializable]
public record ProcessingAchRefunds : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
