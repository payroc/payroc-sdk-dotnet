using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Bank;

[Serializable]
public record BankAccountVerificationRequest
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Polymorphic object that contains bank account information.
    ///
    /// The value of the type field determines which variant you should use:
    /// -	`ach` - Automated Clearing House (ACH) details
    /// -	`pad` - Pre-authorized debit (PAD) details
    /// </summary>
    [JsonPropertyName("bankAccount")]
    public required BankAccountVerificationRequestBankAccount BankAccount { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
