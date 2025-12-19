using Payroc.RepeatPayments.PaymentPlans;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;

namespace Payroc.TestFunctional.RepeatPayments.PaymentPlans;

[TestFixture, Category("RepeatPayments.PaymentPlans")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request =  Data.Get<CreatePaymentPlansRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, PaymentPlansFactory.Create())
        ]);
        var response = await client.RepeatPayments.PaymentPlans.CreateAsync(request);
        var retrieveRequest = new RetrievePaymentPlansRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            PaymentPlanId = response.PaymentPlanId
        };

        var retrieveResponse = await client.RepeatPayments.PaymentPlans.RetrieveAsync(retrieveRequest);

        Assert.That(retrieveResponse.PaymentPlanId, Is.Not.Null);
    }
}
