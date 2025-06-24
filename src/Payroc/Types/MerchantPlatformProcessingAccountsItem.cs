using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record MerchantPlatformProcessingAccountsItem
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonPropertyName("processingAccountId")]
    public string? ProcessingAccountId { get; set; }

    /// <summary>
    /// Trading name of the business.
    /// </summary>
    [JsonPropertyName("doingBusinessAs")]
    public string? DoingBusinessAs { get; set; }

    /// <summary>
    /// Status of the processing account.
    /// - `entered` - We have received information about the account, but we have not yet reviewed it.
    /// - `pending` - We have reviewed the information about the account, but we have not yet approved it.
    /// - `approved` - We have approved the account for processing transactions and funding.
    /// - `subjectTo` - We have approved the account, but we are waiting on further information.
    /// - `dormant` - Account is closed for a period.
    /// - `nonProcessing` - We have approved the account, but the merchant has not yet run a transaction.
    /// - `rejected` - We rejected the application for the processing account.
    /// - `terminated` - Processing account is closed.
    /// - `cancelled` - Merchant withdrew the application for the processing account.
    /// - `failed` - An error occurred while we were setting up the processing account.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public MerchantPlatformProcessingAccountsItemStatus? Status { get; set; }

    /// <summary>
    /// Object that contains HATEOAS links for the processing account.
    /// </summary>
    [JsonPropertyName("link")]
    public MerchantPlatformProcessingAccountsItemLink? Link { get; set; }

    [JsonPropertyName("signature")]
    public Signature? Signature { get; set; }

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
