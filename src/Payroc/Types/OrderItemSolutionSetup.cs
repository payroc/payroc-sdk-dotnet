using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the settings for the solution, including gateway settings, device settings, and application settings.
/// </summary>
[Serializable]
public record OrderItemSolutionSetup
{
    [JsonPropertyName("timezone")]
    public SchemasTimezone? Timezone { get; set; }

    /// <summary>
    /// Unique identifier of the industry template you want to apply to the solution. Send one of the following values:
    /// - `Retail`
    /// - `Restaurant`
    /// - `Moto`
    /// - `Ecommerce`
    /// </summary>
    [JsonPropertyName("industryTemplateId")]
    public string? IndustryTemplateId { get; set; }

    /// <summary>
    /// Object that contains the gateway settings for the solution.
    /// </summary>
    [JsonPropertyName("gatewaySettings")]
    public OrderItemSolutionSetupGatewaySettings? GatewaySettings { get; set; }

    /// <summary>
    /// Object that contains the application settings for the solution.
    /// </summary>
    [JsonPropertyName("applicationSettings")]
    public OrderItemSolutionSetupApplicationSettings? ApplicationSettings { get; set; }

    /// <summary>
    /// Object that contains the device settings if the solution includes a terminal or a peripheral device such as a printer.
    /// </summary>
    [JsonPropertyName("deviceSettings")]
    public OrderItemSolutionSetupDeviceSettings? DeviceSettings { get; set; }

    /// <summary>
    /// Object that contains information about when and how the terminal closes the batch.
    /// </summary>
    [JsonPropertyName("batchClosure")]
    public OrderItemSolutionSetupBatchClosure? BatchClosure { get; set; }

    /// <summary>
    /// Object that indicates if the terminal can send email receipts, text receipts, or both.
    /// </summary>
    [JsonPropertyName("receiptNotifications")]
    public OrderItemSolutionSetupReceiptNotifications? ReceiptNotifications { get; set; }

    /// <summary>
    /// Array of tax objects that contains the taxes that apply to the merchant's transactions.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<OrderItemSolutionSetupTaxesItem>? Taxes { get; set; }

    /// <summary>
    /// Object that contains the tip options for transactions ran on the terminal.
    /// </summary>
    [JsonPropertyName("tips")]
    public OrderItemSolutionSetupTips? Tips { get; set; }

    /// <summary>
    /// Indicates if the terminal can tokenize customer's payment details. For more information about tokenization, go to [Tokenization.](https://docs.payroc.com/knowledge/basic-concepts/tokenization)
    /// </summary>
    [JsonPropertyName("tokenization")]
    public bool? Tokenization { get; set; }

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
