using Payroc.Funding.FundingRecipients;
using Payroc.TestFunctional.Factories;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

public class ListFundingRecipientOwnersTests
{
    [Test]
    [Ignore("Data issues: Failing KYC checks - needs investigation")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CreateFundingRecipient>([
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        var response = await client.Funding.FundingRecipients.CreateAsync(request);
        var ownerRequest = new ListFundingRecipientOwnersRequest()
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null"),
        };
        
        var ownerResponse = await client.Funding.FundingRecipients.ListOwnersAsync(ownerRequest);
        
        Assert.That(ownerResponse.First().OwnerId, Is.Not.Null);
    }
}
