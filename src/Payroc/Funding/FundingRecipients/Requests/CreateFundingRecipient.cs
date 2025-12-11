using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

[Serializable]
public record CreateFundingRecipient
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Type or legal structure of the funding recipient.
    /// </summary>
    [JsonPropertyName("recipientType")]
    public required CreateFundingRecipientRecipientType RecipientType { get; set; }

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
    /// Trading name of the business or organization.
    /// </summary>
    [JsonPropertyName("doingBusinessAs")]
    public required string DoingBusinessAs { get; set; }

    /// <summary>
    /// Address of the funding recipient.
    /// </summary>
    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Array of contactMethod objects that you can use to add contact methods for the funding recipient. You must provide at least an email address.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    /// <summary>
    /// [Metadata](https://docs.payroc.com/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of owner objects. Each object contains information about an individual who owns or manages the funding recipient.
    /// </summary>
    [JsonPropertyName("owners")]
    public IEnumerable<Owner> Owners { get; set; } = new List<Owner>();

    /// <summary>
    /// Array of fundingAccount objects that you can use to add funding accounts to the funding recipient.
    /// </summary>
    [JsonPropertyName("fundingAccounts")]
    public IEnumerable<FundingAccount> FundingAccounts { get; set; } = new List<FundingAccount>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
