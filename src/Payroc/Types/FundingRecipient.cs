using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record FundingRecipient
{
    /// <summary>
    /// Unique identifier of the funding recipient.
    /// </summary>
    [JsonPropertyName("recipientId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? RecipientId { get; set; }

    /// <summary>
    /// Indicates if we have approved the funding recipient.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public FundingRecipientStatus? Status { get; set; }

    /// <summary>
    /// Date the funding recipient was created.
    /// </summary>
    [JsonPropertyName("createdDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date the funding recipient was last modified.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Type or legal structure of the funding recipient.
    /// </summary>
    [JsonPropertyName("recipientType")]
    public required FundingRecipientRecipientType RecipientType { get; set; }

    /// <summary>
    /// Employer identification number (EIN) or Social Security number (SSN).
    /// </summary>
    [JsonPropertyName("taxId")]
    public required string TaxId { get; set; }

    /// <summary>
    /// Government identifier of the charity.
    /// </summary>
    [JsonPropertyName("charityId")]
    public string? CharityId { get; set; }

    /// <summary>
    /// Legal name that the business operates under.
    /// </summary>
    [JsonPropertyName("doingBuinessAs")]
    public required string DoingBuinessAs { get; set; }

    /// <summary>
    /// Address of the funding recipient.
    /// </summary>
    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Array of contactMethod objects for the funding recipient.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    /// <summary>
    /// [Metadata](/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of owner objects associated with the funding recipient.
    /// </summary>
    [JsonPropertyName("owners")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public IEnumerable<FundingRecipientOwnersItem> Owners { get; set; } =
        new List<FundingRecipientOwnersItem>();

    /// <summary>
    /// Array of fundingAccount objects associated with the funding recipient.
    /// </summary>
    [JsonPropertyName("fundingAccounts")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public IEnumerable<FundingRecipientFundingAccountsItem> FundingAccounts { get; set; } =
        new List<FundingRecipientFundingAccountsItem>();

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
