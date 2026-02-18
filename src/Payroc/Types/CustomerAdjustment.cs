using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the adjustment to the transaction. Send this object if the merchant is adjusting the customer’s contact details.
/// </summary>
[Serializable]
public record CustomerAdjustment : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("shippingAddress")]
    public Shipping? ShippingAddress { get; set; }

    /// <summary>
    /// Array of polymorphic objects, which contain contact information.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`email` - Email address
    /// -	`phone` - Phone number
    /// -	`mobile` - Mobile number
    /// -	`fax` - Fax number
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
