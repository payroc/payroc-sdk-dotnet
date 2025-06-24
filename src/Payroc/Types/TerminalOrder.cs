using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record TerminalOrder
{
    /// <summary>
    /// Unique identifier of the terminal order.
    /// </summary>
    [JsonPropertyName("terminalOrderId")]
    public required string TerminalOrderId { get; set; }

    /// <summary>
    /// Status of the terminal order.
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
    /// Date that we received the terminal order.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date that the terminal order was last changed.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

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
