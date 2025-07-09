using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer.
/// </summary>
[Serializable]
public record BankTransferCustomer : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Customer's preferred notification language. This code follows the ISO 639-1 standard.
    /// </summary>
    [JsonPropertyName("notificationLanguage")]
    public BankTransferCustomerNotificationLanguage? NotificationLanguage { get; set; }

    /// <summary>
    /// Customer's contact information.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod>? ContactMethods { get; set; }

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
