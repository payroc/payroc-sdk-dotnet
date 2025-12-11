using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
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
        _ = await client.Funding.FundingRecipients.CreateAsync(request);
        _ = await client.Funding.FundingRecipients.CreateAsync(request);
        var ListRequest = new ListFundingRecipientsRequest()
        {
            Limit = 10
        };
        
       var listResponse = await client.Funding.FundingRecipients.ListAsync(ListRequest);
        
       Assert.That(listResponse.CurrentPage.Items.Count, Is.GreaterThan(2));
    }
}
