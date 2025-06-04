using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the feature settings for the terminal.
/// </summary>
public record ProcessingTerminalFeatures
{
    [JsonPropertyName("tips")]
    public ProcessingTerminalFeaturesTips? Tips { get; set; }

    /// <summary>
    /// Object that contains details about level two and level three transactions.
    /// </summary>
    [JsonPropertyName("enhancedProcessing")]
    public required ProcessingTerminalFeaturesEnhancedProcessing EnhancedProcessing { get; set; }

    /// <summary>
    /// Object that contains details about EBT transactions.
    /// </summary>
    [JsonPropertyName("ebt")]
    public required OneOf<EbtEnabled, EbtDisabled> Ebt { get; set; }

    /// <summary>
    /// Indicates if the terminal prompts for cashback on PIN debit transactions.
    /// </summary>
    [JsonPropertyName("pinDebitCashback")]
    public required bool PinDebitCashback { get; set; }

    /// <summary>
    /// Indicates if the terminal can run repeat payments. For more information about repeat payments, go to [Payment Plans](/guides/integrate/repeat-payments).
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
    /// Indicates if the terminal can accept payments when it can't connect to the gateway. For more information about offline processing, go to [Offline Processing](/knowledge/card-payments/offline-processing).
    /// </summary>
    [JsonPropertyName("offlinePayments")]
    public bool? OfflinePayments { get; set; }

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
