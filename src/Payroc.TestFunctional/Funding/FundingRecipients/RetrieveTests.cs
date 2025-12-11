using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
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
        var retrieveRequest = new RetrieveFundingRecipientsRequest()
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null")
        };
        
        var retrieveResponse = await client.Funding.FundingRecipients.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.RecipientId, Is.Not.Null);
    }
}
