using Payroc.Funding.FundingActivity;
using Payroc.Funding.FundingInstructions;
using Payroc.Funding.FundingRecipients;
using Payroc.TestFunctional.Factories.Funding.RequestBodies;

namespace Payroc.TestFunctional.Funding.FundingInstructions;

[TestFixture, Category("Funding.FundingInstructions")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
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
        var fundingAccountResponse = await client.Funding.FundingRecipients.CreateAccountAsync(createFundingAccountRequest);
        var listFundingBalances = new RetrieveBalanceFundingActivityRequest
        {
            Limit = 10
        };
        var balancesResponse = await client.Funding.FundingActivity.RetrieveBalanceAsync(listFundingBalances);
        var fundingInstructionRequest = new CreateFundingInstructionsRequest
        {
            IdempotencyKey =  Guid.NewGuid().ToString(),
            Body = FundingInstructionsFactory.Create(balancesResponse.Data.First().MerchantId, fundingAccountResponse.FundingAccountId)
        };
        
        var fundingInstructionResponse = await client.Funding.FundingInstructions.CreateAsync(fundingInstructionRequest);
        
        Assert.That(fundingInstructionResponse.InstructionId, Is.Not.Null);
    }
}
