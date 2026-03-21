using Payroc.PaymentLinks;
using Payroc.TestCommon.Factories.Payments;

namespace Payroc.TestCommon.Tests.PaymentLinks;

[TestFixture, Category("PaymentLinks")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task PaymentLinks_Retrieve_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest =Data.Get<CreatePaymentLinksRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentLinksFactory.Create())
        ]);
        var createResponse = await client.PaymentLinks.CreateAsync(createRequest);
        var retrieveRequest = new RetrievePaymentLinksRequest()
        {
            PaymentLinkId = createResponse.AsMultiUse().PaymentLinkId ?? string.Empty
        };
        
        var retrieveResponse = await client.PaymentLinks.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.AsMultiUse().PaymentLinkId, Is.EqualTo(createResponse.AsMultiUse().PaymentLinkId));
    }
}
