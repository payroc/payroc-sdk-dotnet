using Payroc.Boarding.ProcessingTerminals;

namespace Payroc.TestCommon.Tests.Boarding.ProcessingTerminals;

[TestFixture, Category("Boarding.ProcessingTerminals")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveHostProcessorConfiguration
{ 
    [Test]
    [Ignore("Data Issue: Terminal does not have host processor configuration available in test environment.")]
    public async Task ProcessingTerminals_RetrieveHostProcessorConfiguration_Success()
    {
        var client = GlobalFixture.Payments;
        var retrieveRequest = new RetrieveHostConfigurationProcessingTerminalsRequest
        {
            ProcessingTerminalId = BoardingTestFixture.SharedProcessingTerminalId
        };
        
        var retrieveResponse = await client.Boarding.ProcessingTerminals.RetrieveHostConfigurationAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.ProcessingTerminalId, Is.EqualTo(BoardingTestFixture.SharedProcessingTerminalId));
    }
}
