using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ProcessingTerminal : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Indicates if the processing terminal is active.
    /// </summary>
    [JsonPropertyName("status")]
    public required ProcessingTerminalStatus Status { get; set; }

    /// <summary>
    /// Time zone of the processing terminal.
    /// </summary>
    [JsonPropertyName("timezone")]
    public required ProcessingTerminalTimezone Timezone { get; set; }

    /// <summary>
    /// Name of the product and its setup.
    /// </summary>
    [JsonPropertyName("program")]
    public string? Program { get; set; }

    /// <summary>
    /// Object that contains the gateway settings for the solution.
    /// </summary>
    [JsonPropertyName("gateway")]
    public PayrocGateway? Gateway { get; set; }

    /// <summary>
    /// Object that contains information about when and how the terminal closes the batch.
    /// </summary>
    [JsonPropertyName("batchClosure")]
    public required ProcessingTerminalBatchClosure BatchClosure { get; set; }

    /// <summary>
    /// Object that contains the application settings for the solution.
    /// </summary>
    [JsonPropertyName("applicationSettings")]
    public ProcessingTerminalApplicationSettings? ApplicationSettings { get; set; }

    /// <summary>
    /// Object that contains the feature settings for the terminal.
    /// </summary>
    [JsonPropertyName("features")]
    public required ProcessingTerminalFeatures Features { get; set; }

    /// <summary>
    /// Array of tax objects that contains the taxes that apply to the merchant's transactions.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<ProcessingTerminalTaxesItem>? Taxes { get; set; }

    /// <summary>
    /// Object that contains the tokenization settings and AVS settings for the terminal.
    /// </summary>
    [JsonPropertyName("security")]
    public ProcessingTerminalSecurity? Security { get; set; }

    /// <summary>
    /// Object that indicates if the terminal can send email receipts or text receipts.
    /// </summary>
    [JsonPropertyName("receiptNotifications")]
    public ProcessingTerminalReceiptNotifications? ReceiptNotifications { get; set; }

    [JsonPropertyName("devices")]
    public IEnumerable<ProcessingTerminalDevicesItem>? Devices { get; set; }

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
