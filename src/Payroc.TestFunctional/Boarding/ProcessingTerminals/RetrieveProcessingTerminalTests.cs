using Payroc.Boarding.ProcessingTerminals;

namespace Payroc.TestFunctional.Boarding.ProcessingTerminals;

[TestFixture, Category("Boarding.ProcessingTerminals")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveProcessingTerminalTests
{
    [Test]
    [Ignore("Data Issues: Currently failing due to TerminalOrders not returning a ProcessingTerminalId. " +
            "This is due to having to use a non-physical device due to not being able link a paymentIntent to a terminal." +
            "Also using an already known existing processingTerminalId throws a Json Deserialization error for batchClosure.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var retrieveRequest = new RetrieveProcessingTerminalsRequest
        {
            ProcessingTerminalId = BoardingTestFixture.SharedProcessingTerminalId
        };
        
        var retrieveResponse = await client.Boarding.ProcessingTerminals.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.ProcessingTerminalId, Is.EqualTo(BoardingTestFixture.SharedProcessingTerminalId));
    }
}