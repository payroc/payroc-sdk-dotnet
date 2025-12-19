using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the customer's contact details and address information.
/// </summary>
[Serializable]
public record Customer : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Identifier of the transaction, also known as a customer code.
    ///
    /// For requests, you must send a value for **referenceNumber** if the customer provides one.
    /// </summary>
    [JsonPropertyName("referenceNumber")]
    public string? ReferenceNumber { get; set; }

    /// <summary>
    /// Object that contains information about the address that the card is registered to.
    /// </summary>
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
    /// Language that the customer uses for notifications. This code follows the [ISO 639-1](https://www.iso.org/iso-639-language-code) alpha-2 standard.
    /// </summary>
    [JsonPropertyName("notificationLanguage")]
    public CustomerNotificationLanguage? NotificationLanguage { get; set; }

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
