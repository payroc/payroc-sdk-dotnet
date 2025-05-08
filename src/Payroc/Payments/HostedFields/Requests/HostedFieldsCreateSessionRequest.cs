using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.HostedFields;

public record HostedFieldsCreateSessionRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Version of the Hosted Fields JavaScript library that you are using.
    ///
    /// The current production version is `1.3.0.135534`.
    /// </summary>
    [JsonPropertyName("libVersion")]
    public required string LibVersion { get; set; }

    /// <summary>
    /// Indicates if a merchant wants to take a payment or tokenize a customer's payment details:
    /// - `payment` - The merchant wants to take a payment immediately.
    /// - `tokenization` - The merchant wants to save the customer's payment details to take a payment later or to update a customer's payment details that they've already saved.
    /// </summary>
    [JsonPropertyName("scenario")]
    public required HostedFieldsCreateSessionRequestScenario Scenario { get; set; }

    /// <summary>
    /// Unique identifier that represents a customer's payment details.
    ///
    /// If a merchant wants to update a customer's payment details that are linked to a secure token, include the secureTokenId in your request.
    /// </summary>
    [JsonPropertyName("secureTokenId")]
    public string? SecureTokenId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
