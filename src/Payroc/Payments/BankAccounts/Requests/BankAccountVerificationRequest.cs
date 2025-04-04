using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankAccounts;

public record BankAccountVerificationRequest
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Object that contains information about the bank account.
    /// </summary>
    [JsonPropertyName("bankAccount")]
    public required BankAccountVerificationRequestBankAccount BankAccount { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
