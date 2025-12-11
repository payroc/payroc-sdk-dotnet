using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the password settings when running specific transaction types.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupApplicationSettingsSecurity : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the terminal should prompt the clerk for a password when running a refund.
    /// </summary>
    [JsonPropertyName("refundPassword")]
    public bool? RefundPassword { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt the clerk for a password when running a keyed sale.
    /// </summary>
    [JsonPropertyName("keyedSalePassword")]
    public bool? KeyedSalePassword { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt the clerk for a password when cancelling a transaction.
    /// </summary>
    [JsonPropertyName("reversalPassword")]
    public bool? ReversalPassword { get; set; }

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
