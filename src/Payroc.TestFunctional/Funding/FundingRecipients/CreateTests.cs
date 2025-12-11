using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingRecipients;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    [Ignore("Data issues: Failing KYC checks - needs investigation")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CreateFundingRecipient>([
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        try
        {
            var response = await client.Funding.FundingRecipients.CreateAsync(request);
        
            Assert.That(response.RecipientId, Is.Not.Null);
        }
        catch (BadRequestError e)
        {
            Assert.Fail($"BadRequestError: {e.Message}");
        }
    }
}
