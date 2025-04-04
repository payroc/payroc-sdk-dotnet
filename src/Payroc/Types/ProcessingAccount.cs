using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record ProcessingAccount
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonPropertyName("processingAccountId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public string? ProcessingAccountId { get; set; }

    /// <summary>
    /// Date that the processing account was created.
    /// </summary>
    [JsonPropertyName("createdDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date that the processing account was last modified.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? LastModifiedDate { get; set; }

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
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public ProcessingAccountStatus? Status { get; set; }

    /// <summary>
    /// Trading name of the business.
    /// </summary>
    [JsonPropertyName("doingBusinessAs")]
    public required string DoingBusinessAs { get; set; }

    /// <summary>
    /// Object that contains information about the owners of the business.
    /// </summary>
    [JsonPropertyName("owners")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public IEnumerable<ProcessingAccountOwnersItem> Owners { get; set; } =
        new List<ProcessingAccountOwnersItem>();

    /// <summary>
    /// Website address of the business.
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// Type of business.
    /// </summary>
    [JsonPropertyName("businessType")]
    public required ProcessingAccountBusinessType BusinessType { get; set; }

    /// <summary>
    /// Category code for the type of business.
    /// </summary>
    [JsonPropertyName("categoryCode")]
    public required int CategoryCode { get; set; }

    /// <summary>
    /// Description of the services or merchandise sold by the business.
    /// </summary>
    [JsonPropertyName("merchandiseOrServiceSold")]
    public required string MerchandiseOrServiceSold { get; set; }

    /// <summary>
    /// Date that the business was established. The format of the value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("businessStartDate")]
    public string? BusinessStartDate { get; set; }

    /// <summary>
    /// Time zone for the processing account.
    /// </summary>
    [JsonPropertyName("timezone")]
    public required ProcessingAccountTimezone Timezone { get; set; }

    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Array of contactMethods objects for the processing account. Atleast one contactMethod must be an email address.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    [JsonPropertyName("processing")]
    public required Processing Processing { get; set; }

    [JsonPropertyName("funding")]
    public required Funding.Funding Funding { get; set; }

    /// <summary>
    /// Object that contains pricing information.
    /// </summary>
    [JsonPropertyName("pricing")]
    public required ProcessingAccountPricing Pricing { get; set; }

    /// <summary>
    /// Array of contact objects.
    /// </summary>
    [JsonPropertyName("contacts")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public IEnumerable<ProcessingAccountContactsItem>? Contacts { get; set; }

    [JsonPropertyName("signature")]
    public required Signature Signature { get; set; }

    /// <summary>
    /// Object that you can send to include custom data in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of useful links related to your request.
    /// </summary>
    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
