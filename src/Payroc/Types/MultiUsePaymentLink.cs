using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about a multi-use payment link.
/// </summary>
public record MultiUsePaymentLink
{
    /// <summary>
    /// Unique identifier that we assigned to the payment link.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("paymentLinkId")]
    public string? PaymentLinkId { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the payment.
    /// </summary>
    [JsonPropertyName("merchantReference")]
    public required string MerchantReference { get; set; }

    [JsonPropertyName("order")]
    public required MultiUsePaymentLinkOrder Order { get; set; }

    /// <summary>
    /// Type of transaction.
    /// </summary>
    [JsonPropertyName("authType")]
    public required MultiUsePaymentLinkAuthType AuthType { get; set; }

    /// <summary>
    /// Payment methods that the merchant accepts.
    /// **Note:** If a payment is a pre-authorization, the customer must pay by card.
    /// </summary>
    [JsonPropertyName("paymentMethods")]
    public IEnumerable<MultiUsePaymentLinkPaymentMethodsItem> PaymentMethods { get; set; } =
        new List<MultiUsePaymentLinkPaymentMethodsItem>();

    /// <summary>
    /// Array of customLabel objects.
    /// **Note:** You can change the label of the payment button only.
    /// </summary>
    [JsonPropertyName("customLabels")]
    public IEnumerable<CustomLabel>? CustomLabels { get; set; }

    [JsonPropertyName("assets")]
    public PaymentLinkAssets? Assets { get; set; }

    /// <summary>
    /// Status of the payment link. The value is one of the following:
    /// - `active` - Payment link is active.
    /// - `completed` - Customer has paid.
    /// - `deactivated` - Merchant has deactivated the link.
    /// - `expired` - Payment link has expired.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public MultiUsePaymentLinkStatus? Status { get; set; }

    /// <summary>
    /// Date that the merchant created the link. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdOn")]
    public DateOnly? CreatedOn { get; set; }

    /// <summary>
    /// Last date that the customer can use the payment link. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("expiresOn")]
    public DateOnly? ExpiresOn { get; set; }

    [JsonPropertyName("credentialOnFile")]
    public CredentialOnFile? CredentialOnFile { get; set; }

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
