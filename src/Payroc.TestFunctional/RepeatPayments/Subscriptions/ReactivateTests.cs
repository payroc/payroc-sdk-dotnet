using Payroc.RepeatPayments.PaymentPlans;
using Payroc.RepeatPayments.Subscriptions;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;
using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.RepeatPayments.Subscriptions;

[TestFixture, Category("RepeatPayments.Subscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class ReactivateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var tokenRequest = Data.Get<TokenizationRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        var tokenResponse =  await client.Tokenization.SecureTokens.CreateAsync(tokenRequest);
        var paymentPlanRequest = Data.Get<CreatePaymentPlansRequest>([
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.Body, PaymentPlansFactory.Create())
        ]); 
        var paymentPlanResponse = await client.RepeatPayments.PaymentPlans.CreateAsync(paymentPlanRequest);
        var subscriptionRequest = Data.Get<SubscriptionRequest>(
        [
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.PaymentPlanId, paymentPlanResponse.PaymentPlanId),
            (i => i.SubscriptionId, Guid.NewGuid().ToString()),
            (i => i.StartDate, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))),
            (i => i.EndDate, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))),
            (i => i.PaymentMethod, new SubscriptionRequestPaymentMethod(
                new SecureTokenPayload 
                { 
                    Token =  tokenResponse.Token
                }))
        ]);
        var subscriptionResponse = await client.RepeatPayments.Subscriptions.CreateAsync(subscriptionRequest);
        var deactivateRequest = new DeactivateSubscriptionsRequest
        {
            SubscriptionId = subscriptionResponse.SubscriptionId,
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs
        };
        var deactivateResponse = await client.RepeatPayments.Subscriptions.DeactivateAsync(deactivateRequest);
        var reactivateRequest = new ReactivateSubscriptionsRequest
        {
            SubscriptionId = deactivateResponse.SubscriptionId,
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs
        };
        
        var reactivateResponse = await client.RepeatPayments.Subscriptions.ReactivateAsync(reactivateRequest);
        
        Assert.That(reactivateResponse.SubscriptionId, Is.Not.Null);
        Assert.That(reactivateResponse.CurrentState.Status, Is.Not.EqualTo(deactivateResponse.CurrentState.Status));
    }
}
