using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Customer contact and address details.
/// </summary>
public record Customer
{
    /// <summary>
    /// Customer's first name.
    /// </summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Customer's last name.
    /// </summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    /// <summary>
    /// Customer's date of birth. The format for this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; set; }

    /// <summary>
    /// Identifier of the transaction, also known as a customer code.
    ///
    /// For requests, you must send a value for **referenceNumber** if the customer provides one.
    /// </summary>
    [JsonPropertyName("referenceNumber")]
    public string? ReferenceNumber { get; set; }

    [JsonPropertyName("billingAddress")]
    public Address? BillingAddress { get; set; }

    [JsonPropertyName("shippingAddress")]
    public Shipping? ShippingAddress { get; set; }

    /// <summary>
    /// Customer's contact information.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod>? ContactMethods { get; set; }

    /// <summary>
    /// Language that the customer uses for notifications.
    ///
    /// This code follows the ISO 639-1 alpha-2 standard.
    /// </summary>
    [JsonPropertyName("notificationLanguage")]
    public CustomerNotificationLanguage? NotificationLanguage { get; set; }

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
