using Payroc.RepeatPayments.PaymentPlans;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;

namespace Payroc.TestFunctional.Payments.PaymentPlans;

[TestFixture, Category("Payments.PaymentPlans")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CreatePaymentPlansRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentPlansFactory.Create())
        ]);

        var response = await client.RepeatPayments.PaymentPlans.CreateAsync(request);

        Assert.That(response.PaymentPlanId, Is.Not.Null);
    }
}
