using Payroc.RepeatPayments.PaymentPlans;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;

namespace Payroc.TestFunctional.RepeatPayments.PaymentPlans;

[TestFixture, Category("RepeatPayments.PaymentPlans")]
[Parallelizable(ParallelScope.Fixtures)]
public class DeleteTests
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
        var createResponse = await client.RepeatPayments.PaymentPlans.CreateAsync(createRequest);
        var deleteRequest = new DeletePaymentPlansRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            PaymentPlanId = createResponse.PaymentPlanId
        };

        // There is no response body for delete, so we just ensure no exception was thrown
        Assert.DoesNotThrowAsync(async () =>
        {
            await client.RepeatPayments.PaymentPlans.DeleteAsync(deleteRequest);
        });
    }
}
