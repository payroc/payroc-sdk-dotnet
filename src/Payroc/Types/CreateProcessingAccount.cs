using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record CreateProcessingAccount
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonPropertyName("processingAccountId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? ProcessingAccountId { get; set; }

    /// <summary>
    /// Trading name of the business.
    /// </summary>
    [JsonPropertyName("doingBusinessAs")]
    public required string DoingBusinessAs { get; set; }

    /// <summary>
    /// Collection of individuals that are responsible for a processing account. When you create a processing account, you must indicate at least one owner as either of the following:
    /// - Control prong - An individual who has a significant equity stake in the business and can make decisions for the processing account. You can add only one control prong to a processing account.
    /// - Authorized signatory - An individual who doesn't have an equity stake in the business but can make decisions for the processing account.
    /// </summary>
    [JsonPropertyName("owners")]
    public IEnumerable<Owner> Owners { get; set; } = new List<Owner>();

    /// <summary>
    /// Website address of the business.
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// Type of business.
    /// </summary>
    [JsonPropertyName("businessType")]
    public required CreateProcessingAccountBusinessType BusinessType { get; set; }

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
    public required string BusinessStartDate { get; set; }

    /// <summary>
    /// Time zone of the processing account.
    /// </summary>
    [JsonPropertyName("timezone")]
    public required CreateProcessingAccountTimezone Timezone { get; set; }

    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Array of contactMethod objects. One contact method must be an email address.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    [JsonPropertyName("processing")]
    public required Processing Processing { get; set; }

    [JsonPropertyName("funding")]
    public required CreateFunding Funding { get; set; }

    [JsonPropertyName("pricing")]
    public required Pricing Pricing { get; set; }

    /// <summary>
    /// Method used to capture the owner's signature.
    ///
    /// **Note:** If you request the owner’s signature by email and they don’t respond, use our Reminders endpoint to create a reminder and to send another email.
    /// </summary>
    [JsonPropertyName("signature")]
    public required CreateProcessingAccountSignature Signature { get; set; }

    /// <summary>
    /// Array of contact objects.
    /// </summary>
    [JsonPropertyName("contacts")]
    public IEnumerable<Contact>? Contacts { get; set; }

    /// <summary>
    /// Object that you can send to include custom data in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

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
