using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that indicates if the terminal can send email receipts or text receipts.
/// </summary>
public record ProcessingTerminalReceiptNotifications
{
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
