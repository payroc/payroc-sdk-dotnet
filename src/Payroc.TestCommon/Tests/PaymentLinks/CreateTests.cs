using Payroc.PaymentLinks;
using Payroc.TestCommon.Factories.Payments;

namespace Payroc.TestCommon.Tests.PaymentLinks;

[TestFixture, Category("PaymentLinks")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task PaymentLinks_Create_Success()
    {
        var client = GlobalFixture.Payments;
        var createPaymentLinksRequest = Data.Get<CreatePaymentLinksRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentLinksFactory.Create())
        ]);
        
        var paymentLinksResponse = await client.PaymentLinks.CreateAsync(createPaymentLinksRequest);
        
        Assert.That(paymentLinksResponse.Type, Is.Not.Null);
    }
}
