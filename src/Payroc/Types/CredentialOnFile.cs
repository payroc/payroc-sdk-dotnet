using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about saving the customer’s payment details.
/// </summary>
public record CredentialOnFile
{
    /// <summary>
    /// Indicates if our gateway should tokenize the customer’s payment details as part of the transaction.
    /// </summary>
    [JsonPropertyName("tokenize")]
    public bool? Tokenize { get; set; }

    /// <summary>
    /// Indicates how the merchant can use the customer’s card details, as agreed by the customer:
    ///
    /// - `unscheduled` - Transactions for a fixed or variable amount that are run at a certain pre-defined event.
    /// - `recurring` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Recurring transactions don’t have a fixed duration and run until the customer cancels the agreement.
    /// - `installment` - Transactions for a fixed amount that are run at regular intervals, for example, monthly. Installment transactions have a fixed duration.
    ///
    /// **Note:** If you send a value for **mitAgreement**, you must send the **standingInstructions** object in the **paymentOrder** object.
    /// </summary>
    [JsonPropertyName("mitAgreement")]
    public CredentialOnFileMitAgreement? MitAgreement { get; set; }

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
