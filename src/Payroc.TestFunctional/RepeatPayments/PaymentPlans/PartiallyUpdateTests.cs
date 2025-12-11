using Payroc.RepeatPayments.PaymentPlans;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;

namespace Payroc.TestFunctional.Payments.PaymentPlans;

[TestFixture, Category("Payments.PaymentPlans")]
[Parallelizable(ParallelScope.Fixtures)]
public class PartiallyUpdateTests
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
        var updateRequest = new PartiallyUpdatePaymentPlansRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            PaymentPlanId = response.PaymentPlanId,
            Body = [
                new(new PatchDocument.Add(new()
                {
                    Path = "/setupOrder/amount",
                    Value = 2999
                }))
            ]
        };

        var updateResponse = await client.RepeatPayments.PaymentPlans.PartiallyUpdateAsync(updateRequest);
        
        Assert.That(updateResponse.PaymentPlanId, Is.EqualTo(response.PaymentPlanId));
        Assert.That(updateResponse.OnUpdate, Is.EqualTo("continue"));
    }
}
