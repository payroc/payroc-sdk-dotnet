using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains details about level two and level three transactions.
/// </summary>
[Serializable]
public record ProcessingTerminalFeaturesEnhancedProcessing
{
    /// <summary>
    /// Indicates if the terminal can run level two and level three transactions.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Indicates if the terminal supports level two or level three transactions.
    /// </summary>
    [JsonPropertyName("transactionDataLevel")]
    public ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel? TransactionDataLevel { get; set; }

    /// <summary>
    /// Indicates the address information that the clerk must provide to qualify for level two or level three data. The value is one of the following:
    /// - `fullAddress` - The clerk must provide the full address for the transaction to qualify.
    /// - `postalCode` - The clerk must provide a postal code for the transaction to qualify.
    /// </summary>
    [JsonPropertyName("shippingAddressMode")]
    public ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode? ShippingAddressMode { get; set; }

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
