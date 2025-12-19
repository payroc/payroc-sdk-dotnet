using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

public class ListFundingRecipientFundingAccountsTests
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
        var listRequest = new ListFundingRecipientFundingAccountsRequest()
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null"),
        };
        
        var listResponse = await client.Funding.FundingRecipients.ListAccountsAsync(listRequest);
        
        Assert.That(listResponse.First().FundingAccountId, Is.Not.Null);
    }
}
