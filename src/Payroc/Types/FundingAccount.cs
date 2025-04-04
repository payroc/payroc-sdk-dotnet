using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record FundingAccount
{
    /// <summary>
    /// Unique identifier of the funding account.
    /// </summary>
    [JsonPropertyName("fundingAccountId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? FundingAccountId { get; set; }

    /// <summary>
    /// Date the funding account was created.
    /// </summary>
    [JsonPropertyName("createdDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date the funding account was last modified.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Status of the funding account.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public FundingAccountStatus? Status { get; set; }

    /// <summary>
    /// Type of funding account.
    /// </summary>
    [JsonPropertyName("type")]
    public required FundingAccountType Type { get; set; }

    /// <summary>
    /// Indicates if we send funds or withdraw funds from the account.
    /// - `credit` - Send funds to the account.
    /// - `debit` - Withdraw funds from the account.
    /// - `creditAndDebit` - Send funds and withdraw funds from the account.
    /// </summary>
    [JsonPropertyName("use")]
    public required FundingAccountUse Use { get; set; }

    /// <summary>
    /// Name of the account holder.
    /// </summary>
    [JsonPropertyName("nameOnAccount")]
    public required string NameOnAccount { get; set; }

    /// <summary>
    /// Array of paymentMethod objects.
    /// </summary>
    [JsonPropertyName("paymentMethods")]
    public IEnumerable<PaymentMethodsItem> PaymentMethods { get; set; } =
        new List<PaymentMethodsItem>();

    /// <summary>
    /// [Metadata](/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of HATEOAS links.
    /// </summary>
    [JsonPropertyName("links")]
    [JsonAccess(JsonAccessType.ReadOnly)]
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
