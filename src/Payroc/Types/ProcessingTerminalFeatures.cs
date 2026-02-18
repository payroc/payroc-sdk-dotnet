using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the feature settings for the terminal.
/// </summary>
[Serializable]
public record ProcessingTerminalFeatures : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Polymorphic object that indicates if the terminal accepts tips.
    ///
    /// The value of the enabled field determines which variant you should use:
    /// -	`true` - Terminal allows tips.
    /// -	`false` - Terminal doesn't allow tips.
    /// </summary>
    [JsonPropertyName("tips")]
    public OneOf<TipProcessingEnabled, TipProcessingDisabled>? Tips { get; set; }

    /// <summary>
    /// Object that contains details about level two and level three transactions.
    /// </summary>
    [JsonPropertyName("enhancedProcessing")]
    public required ProcessingTerminalFeaturesEnhancedProcessing EnhancedProcessing { get; set; }

    /// <summary>
    /// Polymorphic object that indicates if the terminal accepts EBT transactions.
    ///
    /// The value of the enabled field determines which variant you should use:
    /// -	`true` - Terminal allows EBT transactions.
    /// -	`false` - Terminal doesn't allow EBT transactions.
    /// </summary>
    [JsonPropertyName("ebt")]
    public required OneOf<EbtEnabled, EbtDisabled> Ebt { get; set; }

    /// <summary>
    /// Indicates if the terminal prompts for cashback on PIN debit transactions.
    /// </summary>
    [JsonPropertyName("pinDebitCashback")]
    public required bool PinDebitCashback { get; set; }

    /// <summary>
    /// Indicates if the terminal can run repeat payments. For more information about repeat payments, go to [Payment Plans](https://docs.payroc.com/guides/take-payments/repeat-payments).
    /// </summary>
    [JsonPropertyName("recurringPayments")]
    public bool? RecurringPayments { get; set; }

    /// <summary>
    /// Object that contains details about payment links.
    /// </summary>
    [JsonPropertyName("paymentLinks")]
    public ProcessingTerminalFeaturesPaymentLinks? PaymentLinks { get; set; }

    /// <summary>
    /// Indicates if the terminal can run pre-authorizations.
    /// </summary>
    [JsonPropertyName("preAuthorizations")]
    public bool? PreAuthorizations { get; set; }

    /// <summary>
    /// Indicates if the terminal can accept payments when it can't connect to the gateway. For more information about offline processing, go to [Offline Processing](https://docs.payroc.com/knowledge/card-payments/offline-processing).
    /// </summary>
    [JsonPropertyName("offlinePayments")]
    public bool? OfflinePayments { get; set; }

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
