using Payroc.Funding.FundingActivity;
using Payroc.Funding.FundingRecipients;
using Payroc.TestFunctional.Factories.Funding.RequestBodies;

namespace Payroc.TestFunctional.Funding.FundingActivity;

[TestFixture, Category("Funding.FundingActivity")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListFundingActivity
{
    [Test]
    [Ignore("Data issues: Failing KYC checks - needs investigation")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CreateFundingRecipient>([
            (i => i.IdempotencyKey, Guid.NewGuid().ToString())
        ]);
        var response = await client.Funding.FundingRecipients.CreateAsync(request);
        var createFundingAccountRequest = new CreateAccountFundingRecipientsRequest
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null"),
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = FundingAccountFactory.Create()
        };
        _= await client.Funding.FundingRecipients.CreateAccountAsync(createFundingAccountRequest);
        _= await client.Funding.FundingRecipients.CreateAccountAsync(createFundingAccountRequest);
        var listFundingActivityRequest = new ListFundingActivityRequest
        {
            Limit = 10,
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-30)),
            DateTo = DateOnly.FromDateTime(DateTime.Now)
        };
        
        var activityResponse = await client.Funding.FundingActivity.ListAsync(listFundingActivityRequest);
        
        Assert.That(activityResponse.CurrentPage.Items.Count, Is.GreaterThanOrEqualTo(2));
    }
}
