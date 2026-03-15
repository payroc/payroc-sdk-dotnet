using Payroc.Boarding.TerminalOrders;

namespace Payroc.TestCommon.Tests.Boarding.TerminalOrders;

[TestFixture, Category("Boarding.TerminalOrders")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{ 
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var retrieveRequest = new RetrieveTerminalOrdersRequest
        {
            TerminalOrderId = BoardingTestFixture.SharedTerminalOrderId
        };
        
        var retrieveResponse = await client.Boarding.TerminalOrders.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.TerminalOrderId, Is.EqualTo(BoardingTestFixture.SharedTerminalOrderId));
    }
}
