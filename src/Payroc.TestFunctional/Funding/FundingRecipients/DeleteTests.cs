using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class DeleteTests
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
        var deleteRequest = new DeleteFundingRecipientsRequest()
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null")
        };
        
        // No response body returned for delete, so we just ensure no exception is thrown.
        Assert .DoesNotThrowAsync(async () => await client.Funding.FundingRecipients.DeleteAsync(deleteRequest));
    }
}
