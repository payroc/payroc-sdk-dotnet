using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the EBT voucher.
///
/// **Note:** Vouchers are available only for EBT SNAP payments.
/// </summary>
[Serializable]
public record Voucher : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Authorization code that the processor issued for the transaction.
    /// </summary>
    [JsonPropertyName("approvalCode")]
    public required string ApprovalCode { get; set; }

    /// <summary>
    /// Serial number of the voucher.
    /// </summary>
    [JsonPropertyName("serialNumber")]
    public required string SerialNumber { get; set; }

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
