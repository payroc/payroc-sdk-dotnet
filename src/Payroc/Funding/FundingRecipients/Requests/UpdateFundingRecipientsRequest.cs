using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

[Serializable]
public record UpdateFundingRecipientsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the funding recipient.
    /// </summary>
    [JsonIgnore]
    public required int RecipientId { get; set; }

    [JsonIgnore]
    public required FundingRecipient Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
