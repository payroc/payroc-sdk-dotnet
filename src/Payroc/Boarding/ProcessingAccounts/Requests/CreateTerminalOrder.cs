using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[Serializable]
public record CreateTerminalOrder
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingAccountId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonPropertyName("trainingProvider")]
    public TrainingProvider? TrainingProvider { get; set; }

    /// <summary>
    /// Object that contains the shipping details for the terminal order. If you don't provide a shipping address, we use the Doing Business As (DBA) address of the processing account.
    /// </summary>
    [JsonPropertyName("shipping")]
    public CreateTerminalOrderShipping? Shipping { get; set; }

    /// <summary>
    /// One or more items to be ordered
    /// </summary>
    [JsonPropertyName("orderItems")]
    public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
