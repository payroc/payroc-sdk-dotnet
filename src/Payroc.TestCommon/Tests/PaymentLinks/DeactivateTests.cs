using Payroc.PaymentLinks;
using Payroc.TestCommon.Factories.Payments;

namespace Payroc.TestCommon.Tests.PaymentLinks;

[TestFixture, Category("PaymentLinks")]
[Parallelizable(ParallelScope.Fixtures)]
public class DeactivateTests
{
    [Test]
    public async Task PaymentLinks_Deactivate_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<CreatePaymentLinksRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentLinksFactory.Create())
        ]);
        var createResponse = await client.PaymentLinks.CreateAsync(createRequest);
        var deactivateRequest = new DeactivatePaymentLinksRequest
        {
            PaymentLinkId = createResponse.AsMultiUse().PaymentLinkId ?? string.Empty
        };
        
        var deactivateResponse = await client.PaymentLinks.DeactivateAsync(deactivateRequest);
        
        Assert.That(deactivateResponse.AsMultiUse().Status, Is.Not.EqualTo(createResponse.AsMultiUse().Status));
    }
}
