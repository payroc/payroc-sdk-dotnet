using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

/// <summary>
/// Object that contains the shipping address for the terminal order.
/// </summary>
[Serializable]
public record CreateTerminalOrderShippingAddress : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Name of the person receiving the shipment.
    /// </summary>
    [JsonPropertyName("recipientName")]
    public required string RecipientName { get; set; }

    /// <summary>
    /// Name of the business receiving the shipment.
    /// </summary>
    [JsonPropertyName("businessName")]
    public string? BusinessName { get; set; }

    /// <summary>
    /// First line of the shipment address.
    /// </summary>
    [JsonPropertyName("addressLine1")]
    public required string AddressLine1 { get; set; }

    /// <summary>
    /// Second line of the shipment address.
    /// </summary>
    [JsonPropertyName("addressLine2")]
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// City of the shipment address.
    /// </summary>
    [JsonPropertyName("city")]
    public required string City { get; set; }

    /// <summary>
    /// State of the shipment address.
    /// </summary>
    [JsonPropertyName("state")]
    public required string State { get; set; }

    /// <summary>
    /// Postal code of the shipment address.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public required string PostalCode { get; set; }

    /// <summary>
    /// Contact email address for the shipment.
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    /// <summary>
    /// Contact number for the shipment.
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

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
