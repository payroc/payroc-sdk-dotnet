using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that indicates if the terminal can send email receipts, text receipts, or both.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupReceiptNotifications : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the terminal can send receipts by email.
    /// </summary>
    [JsonPropertyName("emailReceipt")]
    public bool? EmailReceipt { get; set; }

    /// <summary>
    /// Indicates if the terminal can send receipts by text message.
    /// </summary>
    [JsonPropertyName("smsReceipt")]
    public bool? SmsReceipt { get; set; }

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
