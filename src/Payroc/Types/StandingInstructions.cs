using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// If you don't use our Subscriptions mechanism, include this section to configure your standing/recurring orders.
/// </summary>
[Serializable]
public record StandingInstructions : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Position of the transaction in the payment plan sequence.
    /// </summary>
    [JsonPropertyName("sequence")]
    public required StandingInstructionsSequence Sequence { get; set; }

    /// <summary>
    /// Indicates the type of payment instruction.
    ///
    /// - 'unscheduled' – The payment is not part of a regular billing cycle.
    /// - 'recurring' – The payment is part of a regular billing cycle with no end date.
    /// - 'installment' – The payment is part of a regular billing cycle with an end date.
    /// </summary>
    [JsonPropertyName("processingModel")]
    public required StandingInstructionsProcessingModel ProcessingModel { get; set; }

    /// <summary>
    /// Object that contains information about the initial payment for the payment instruction.
    /// </summary>
    [JsonPropertyName("referenceDataOfFirstTxn")]
    public FirstTxnReferenceData? ReferenceDataOfFirstTxn { get; set; }

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
