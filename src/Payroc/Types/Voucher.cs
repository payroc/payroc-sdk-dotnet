using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the EBT voucher.
/// **Note:** Voucher is available only for EBT Cash benefit accounts.
/// </summary>
public record Voucher
{
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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
