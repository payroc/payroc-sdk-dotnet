using Payroc.HostedFields;

namespace Payroc.TestCommon.Tests.HostedFields;

[TestFixture, Category("Payments.HostedFields")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task HostedFields_Create_Success()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<HostedFieldsCreateSessionRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.LibVersion, "1.6.0.172429"),
        ]);
        
        var response = await client.HostedFields.CreateAsync(request);
        
        Assert.That(response.Token, Is.Not.Null);
        Assert.That(response.ExpiresAt, Is.Not.Null);
    }
}
