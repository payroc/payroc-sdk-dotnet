using Payroc.Funding.FundingRecipients;
using Payroc.TestCommon.Factories;

namespace Payroc.TestCommon.Tests.Funding.FundingRecipients;

public class CreateRecipientOwnerTests
{
    [Test]
    [Ignore("Data issues: Failing KYC checks - needs investigation")]
    public async Task FundingRecipients_CreateRecipientOwner_Success()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CreateFundingRecipient>([
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        var response = await client.Funding.FundingRecipients.CreateAsync(request);
        var ownerRequest = new CreateOwnerFundingRecipientsRequest()
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null"),
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = OwnerFactory.Create()
        };
        
        var ownerResponse = await client.Funding.FundingRecipients.CreateOwnerAsync(ownerRequest);
        
        Assert.That(ownerResponse.OwnerId, Is.Not.Null);
    }
}
