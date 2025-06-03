using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the information to email a payment link.
/// </summary>
public record PaymentLinkEmailShareEvent
{
    /// <summary>
    /// Method that the merchant uses to share the payment link.
    /// </summary>
    [JsonPropertyName("sharingMethod")]
    public string SharingMethod { get; set; } = "email";

    /// <summary>
    /// Unique identifier that we assigned to the sharing event.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("sharingEventId")]
    public string? SharingEventId { get; set; }

    /// <summary>
    /// Date and time that the merchant shared the link. Our gateway returns this value in the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Indicates if we send a copy of the email to the merchant. By default, we don't send a copy to the merchant.
    /// </summary>
    [JsonAccess(JsonAccessType.WriteOnly)]
    [JsonPropertyName("merchantCopy")]
    public bool? MerchantCopy { get; set; }

    /// <summary>
    /// Message that the merchant sends with the payment link.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Array that contains the recipients of the payment link.
    /// </summary>
    [JsonPropertyName("recipients")]
    public IEnumerable<PaymentLinkEmailRecipient> Recipients { get; set; } =
        new List<PaymentLinkEmailRecipient>();

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
