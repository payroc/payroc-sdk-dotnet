using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Balance
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who requested the balance inquiry.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("card")]
    public required Card Card { get; set; }

    /// <summary>
    /// Response from the processor.
    /// - `A` - The processor approved the transaction.
    /// - `D` - The processor declined the transaction.
    /// - `E` - The processor received the transaction but will process the transaction later.
    /// - `P` - The processor authorized a portion of the original amount of the transaction.
    /// - `R` - The issuer declined the transaction and indicated that the customer should contact their bank.
    /// - `C` - The issuer declined the transaction and indicated that the merchant should keep the card as it was reported lost or stolen.
    /// </summary>
    [JsonPropertyName("responseCode")]
    public BalanceResponseCode? ResponseCode { get; set; }

    /// <summary>
    /// Response description from the payment processor. For example, "Refer to Card Issuer".
    /// </summary>
    [JsonPropertyName("responseMessage")]
    public string? ResponseMessage { get; set; }

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
