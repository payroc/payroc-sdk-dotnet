using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record FundingRecipient : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the funding recipient.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("recipientId")]
    public int? RecipientId { get; set; }

    /// <summary>
    /// Indicates if we have approved the funding recipient.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public FundingRecipientStatus? Status { get; set; }

    /// <summary>
    /// Date the funding recipient was created.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date the funding recipient was last modified.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastModifiedDate")]
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
    [JsonPropertyName("doingBusinessAs")]
    public required string DoingBusinessAs { get; set; }

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
    /// [Metadata](https://docs.payroc.com/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of owner objects associated with the funding recipient.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("owners")]
    public IEnumerable<FundingRecipientOwnersItem>? Owners { get; set; }

    /// <summary>
    /// Array of fundingAccount objects associated with the funding recipient.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("fundingAccounts")]
    public IEnumerable<FundingRecipientFundingAccountsItem>? FundingAccounts { get; set; }

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
