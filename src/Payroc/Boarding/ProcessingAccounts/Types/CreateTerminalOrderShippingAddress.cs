using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

/// <summary>
/// Object that contains the shipping address for the terminal order.
/// </summary>
public record CreateTerminalOrderShippingAddress
{
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
