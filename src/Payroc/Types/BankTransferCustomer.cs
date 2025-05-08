using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer.
/// </summary>
public record BankTransferCustomer
{
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
