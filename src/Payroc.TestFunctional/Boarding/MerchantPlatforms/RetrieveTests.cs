using Payroc.Boarding.MerchantPlatforms;

namespace Payroc.TestFunctional.Boarding.MerchantPlatforms;

[TestFixture, Category("Boarding.MerchantPlatforms")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var retrieveRequest = new RetrieveMerchantPlatformsRequest
        {
            MerchantPlatformId = BoardingTestFixture.SharedMerchantPlatformId
        };
        
        var retrieveResponse = await client.Boarding.MerchantPlatforms.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.MerchantPlatformId, Is.EqualTo(BoardingTestFixture.SharedMerchantPlatformId));
    }
}
