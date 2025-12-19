using Payroc.RepeatPayments.PaymentPlans;
using Payroc.RepeatPayments.Subscriptions;
using Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;
using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.RepeatPayments.Subscriptions;

[TestFixture, Category("RepeatPayments.Subscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class PartiallyUpdateTests
{
    [Test]
    [Ignore("recurringOrder is null in partiallyUpdateAsync response")]
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
        var partiallyUpdateRequest = new PartiallyUpdateSubscriptionsRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            SubscriptionId = subscriptionResponse.SubscriptionId,
            Body = [
                new(new PatchDocument.Replace(new()
                {
                    Path = "/name",
                    Value = "some name"
                }))
            ]
            
        };
        
        var partiallyUpdateResponse = await client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(partiallyUpdateRequest);
        
        Assert.That(partiallyUpdateResponse.SubscriptionId, Is.Not.Null);
    }
}
