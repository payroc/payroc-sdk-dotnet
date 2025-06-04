using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record FundingAccount
{
    /// <summary>
    /// Unique identifier of the funding account.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("fundingAccountId")]
    public int? FundingAccountId { get; set; }

    /// <summary>
    /// Date and time that we received your request to create the funding account in our system.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date and time that the funding account was last modified.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Status of the funding account. The value is one of the following:
    /// - `approved` - We approved the funding account.
    /// - `rejected` - We rejected the funding account.
    /// - `pending` - We have not yet approved the funding account.
    /// - `hold` - Our Risk team have temporarily placed a hold on the funding account.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
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
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

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
