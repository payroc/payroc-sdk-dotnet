using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about a return.
/// </summary>
[Serializable]
public record BankTransferReturnSummary : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that our gateway assigned to the payment.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public required string PaymentId { get; set; }

    /// <summary>
    /// The date that the check was returned.
    /// </summary>
    [JsonPropertyName("date")]
    public required DateOnly Date { get; set; }

    /// <summary>
    /// The NACHA return code.
    /// </summary>
    [JsonPropertyName("returnCode")]
    public required string ReturnCode { get; set; }

    /// <summary>
    /// The reason why the check was returned.
    /// </summary>
    [JsonPropertyName("returnReason")]
    public required string ReturnReason { get; set; }

    /// <summary>
    /// Indicates whether the return has been re-presented.
    /// </summary>
    [JsonPropertyName("represented")]
    public required bool Represented { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
