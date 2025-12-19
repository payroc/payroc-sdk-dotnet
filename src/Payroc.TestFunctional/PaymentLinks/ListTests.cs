using Payroc.PaymentLinks;
using Payroc.TestFunctional.Factories.Payments;

namespace Payroc.TestFunctional.PaymentLinks;

[TestFixture, Category("PaymentLinks")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<CreatePaymentLinksRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentLinksFactory.Create())
        ]);
        _ = await client.PaymentLinks.CreateAsync(createRequest);
        _ = await client.PaymentLinks.CreateAsync(createRequest);
                var request = new ListPaymentLinksRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            Limit = 3
        };

        var response = await client.PaymentLinks.ListAsync(request);

        Assert.That(response.CurrentPage.Count(), Is.GreaterThan(2));
    }
}
