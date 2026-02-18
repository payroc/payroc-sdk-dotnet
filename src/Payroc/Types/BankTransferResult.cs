using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the transaction.
/// </summary>
[Serializable]
public record BankTransferResult : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Type of transaction.
    /// </summary>
    [JsonPropertyName("type")]
    public required BankTransferResultType Type { get; set; }

    /// <summary>
    /// Status of the transaction.
    /// </summary>
    [JsonPropertyName("status")]
    public required BankTransferResultStatus Status { get; set; }

    /// <summary>
    /// Amount of the transaction.
    /// **Note:** The amount is negative for a refund.
    /// </summary>
    [JsonPropertyName("authorizedAmount")]
    public long? AuthorizedAmount { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    /// <summary>
    /// Response from the processor.
    /// - `A` - The processor approved the transaction.
    /// - `D` - The processor declined the transaction.
    /// </summary>
    [JsonPropertyName("responseCode")]
    public required string ResponseCode { get; set; }

    /// <summary>
    /// Description of the response from the processor.
    /// </summary>
    [JsonPropertyName("responseMessage")]
    public string? ResponseMessage { get; set; }

    /// <summary>
    /// Original response code that the processor sent.
    /// </summary>
    [JsonPropertyName("processorResponseCode")]
    public string? ProcessorResponseCode { get; set; }

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
