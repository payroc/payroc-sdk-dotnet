using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var paymentRequest = Data.Get<PaymentRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs ),
            ]);
        _ = await client.CardPayments.Payments.CreateAsync(paymentRequest);
        _ = await client.CardPayments.Payments.CreateAsync(paymentRequest);
        var request = new ListPaymentsRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            Operator = "Jane"
        };

        var response = await client.CardPayments.Payments.ListAsync(request);

        Assert.That(response.CurrentPage.Count(), Is.GreaterThanOrEqualTo(2));
    }
}
