using Payroc.HostedFields;

namespace Payroc.TestFunctional.Payments.HostedFields;

[TestFixture, Category("Payments.HostedFields")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<HostedFieldsCreateSessionRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
        ]);
        
        var response = await client.HostedFields.CreateAsync(request);
        
        Assert.That(response.Token, Is.Not.Null);
        Assert.That(response.ExpiresAt, Is.Not.Null);
    }
}
