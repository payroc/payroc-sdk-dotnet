using Payroc.Boarding.Owners;

namespace Payroc.TestFunctional.Boarding.Owners;

[TestFixture, Category("Boarding.Owners")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var ownersRequest = new RetrieveOwnersRequest
        {
            OwnerId = int.Parse(BoardingTestFixture.SharedOwnerId)
        };
        
        var ownersResponse = await client.Boarding.Owners.RetrieveAsync(ownersRequest);
        
        Assert.That(ownersResponse.OwnerId?.ToString(), Is.EqualTo(BoardingTestFixture.SharedOwnerId));
    }
}
