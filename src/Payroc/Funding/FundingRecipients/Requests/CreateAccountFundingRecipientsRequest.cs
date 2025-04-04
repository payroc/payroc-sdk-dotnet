using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

public record CreateAccountFundingRecipientsRequest
{
    /// <summary>
    /// Unique identifier of the funding recipient.
    /// </summary>
    [JsonIgnore]
    public required int RecipientId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonIgnore]
    public required FundingAccount Body { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
