namespace Payroc.TestFunctional.Payments.ApplePaySessions;

[TestFixture, Category("Payments.ApplePaySessions")]
[Parallelizable(ParallelScope.Fixtures)]
[Ignore("Data Errors: Processing terminal does not support Gateway Managed certs")]
public class StartTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<Payroc.ApplePaySessions.ApplePaySessions>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);

        try
        {
            var response = await client.ApplePaySessions.CreateAsync(request);
            Assert.That(response.StartSessionResponse, Is.Not.Null);
        }
        catch (BadRequestError e)
        {
            Assert.Fail($"Exception occurred: {e.Message}");
        }
    }
}