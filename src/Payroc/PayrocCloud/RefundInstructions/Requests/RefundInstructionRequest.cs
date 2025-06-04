using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PayrocCloud.RefundInstructions;

public record RefundInstructionRequest
{
    /// <summary>
    /// Serial number that identifies the merchant’s payment device.
    /// </summary>
    [JsonIgnore]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("order")]
    public required RefundInstructionOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("ipAddress")]
    public IpAddress? IpAddress { get; set; }

    [JsonPropertyName("customizationOptions")]
    public CustomizationOptions? CustomizationOptions { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
