using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record TerminalOrder : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the terminal order.
    /// </summary>
    [JsonPropertyName("terminalOrderId")]
    public required string TerminalOrderId { get; set; }

    /// <summary>
    /// Status of the terminal order.
    ///
    /// **Note**: You can subscribe to our terminalOrder.status.changed event to get notifications when we update the status of a terminal order. For more information about how to subscribe to events, go to [Event Subscriptions](https://docs.payroc.com/guides/board-merchants/event-subscriptions).
    /// </summary>
    [JsonPropertyName("status")]
    public required TerminalOrderStatus Status { get; set; }

    /// <summary>
    /// Indicates who provides training to the merchant for the solution.
    /// </summary>
    [JsonPropertyName("trainingProvider")]
    public TerminalOrderTrainingProvider? TrainingProvider { get; set; }

    /// <summary>
    /// Object that contains the shipping details for the terminal order. If you don't provide a shipping address, we use the Doing Business As (DBA) address of the processing account.
    /// </summary>
    [JsonPropertyName("shipping")]
    public TerminalOrderShipping? Shipping { get; set; }

    /// <summary>
    /// Array of orderItem objects. Provide a minimum of 1 order item and a maximum of 20 order items.
    /// </summary>
    [JsonPropertyName("orderItems")]
    public IEnumerable<TerminalOrderOrderItemsItem> OrderItems { get; set; } =
        new List<TerminalOrderOrderItemsItem>();

    /// <summary>
    /// Date that we received the terminal order. We return this value in the [ISO-8601](https://www.iso.org/iso-8601-date-and-time-format.html) format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date that the terminal order was last changed.  We return this value in the [ISO-8601](https://www.iso.org/iso-8601-date-and-time-format.html) format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

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
