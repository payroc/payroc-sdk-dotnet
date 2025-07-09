using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record Balance : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
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
    /// Response description from the payment processor, for example, Refer to Card Issuer.
    /// </summary>
    [JsonPropertyName("responseMessage")]
    public string? ResponseMessage { get; set; }

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
