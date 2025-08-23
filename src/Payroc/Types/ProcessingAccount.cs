using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ProcessingAccount : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("processingAccountId")]
    public string? ProcessingAccountId { get; set; }

    /// <summary>
    /// Date and time that we received your request to create the processing account in our system.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date and time that the processing account was last modified.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastModifiedDate")]
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
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public ProcessingAccountStatus? Status { get; set; }

    /// <summary>
    /// Trading name of the business.
    /// </summary>
    [JsonPropertyName("doingBusinessAs")]
    public required string DoingBusinessAs { get; set; }

    /// <summary>
    /// Object that contains information about the owners of the business.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("owners")]
    public IEnumerable<ProcessingAccountOwnersItem>? Owners { get; set; }

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
    /// Merchant Category Code (MCC) for the type of business.
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
    public DateOnly? BusinessStartDate { get; set; }

    [JsonPropertyName("timezone")]
    public required Timezone Timezone { get; set; }

    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Array of contactMethods objects for the processing account. At least one contactMethod must be an email address.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    [JsonPropertyName("processing")]
    public required Processing Processing { get; set; }

    [JsonPropertyName("funding")]
    public required Funding.Funding Funding { get; set; }

    /// <summary>
    /// Object that HATEOAS links to the pricing information that we apply to the processing account.
    /// </summary>
    [JsonPropertyName("pricing")]
    public required ProcessingAccountPricing Pricing { get; set; }

    /// <summary>
    /// Array of contact objects.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("contacts")]
    public IEnumerable<ProcessingAccountContactsItem>? Contacts { get; set; }

    [JsonPropertyName("signature")]
    public required Signature Signature { get; set; }

    /// <summary>
    /// Object that you can send to include custom data in the request. For more information about how to use metadata, go to [Metadata](https://docs.payroc.com/api/metadata).
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of useful links related to your request.
    /// </summary>
    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

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
