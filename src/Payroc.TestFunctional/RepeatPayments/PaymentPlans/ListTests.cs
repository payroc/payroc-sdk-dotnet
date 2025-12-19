using Payroc.RepeatPayments.PaymentPlans;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;

namespace Payroc.TestFunctional.RepeatPayments.PaymentPlans;

[TestFixture, Category("RepeatPayments.PaymentPlans")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRequest =  Data.Get<CreatePaymentPlansRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentPlansFactory.Create())
        ]);
        _ = await client.RepeatPayments.PaymentPlans.CreateAsync(createRequest);
        _ = await client.RepeatPayments.PaymentPlans.CreateAsync(createRequest);
        var request = new ListPaymentPlansRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs
        };

        var response = await client.RepeatPayments.PaymentPlans.ListAsync(request);

        Assert.That(response.CurrentPage.Count(), Is.GreaterThanOrEqualTo(2));
        Assert.That(response.HasNextPage, Is.True);
    }
}
