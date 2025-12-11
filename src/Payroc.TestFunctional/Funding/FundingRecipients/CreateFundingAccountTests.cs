using Payroc.Funding.FundingRecipients;
using Payroc.TestFunctional.Factories.Funding.RequestBodies;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateFundingAccountTests
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
        var createFundingAccountRequest = new CreateAccountFundingRecipientsRequest()
        {
            RecipientId = response.RecipientId ?? throw new Exception("Recipient ID is null"),
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = FundingAccountFactory.Create()
        };
        
        var fundingAccountResponse = await client.Funding.FundingRecipients.CreateAccountAsync(createFundingAccountRequest);
        
        Assert.That(fundingAccountResponse.FundingAccountId, Is.Not.Null);
    }
}