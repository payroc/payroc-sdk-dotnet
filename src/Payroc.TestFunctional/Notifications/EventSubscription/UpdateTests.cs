using Payroc.Notifications.EventSubscriptions;
using Payroc.TestFunctional.Factories.Notifications.EventSubscriptions.RequestBodies;

namespace Payroc.TestFunctional.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class UpdateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = new CreateEventSubscriptionsRequest()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = EventSubscriptionFactory.Create()
        };
        var response = await client.Notifications.EventSubscriptions.CreateAsync(request);
        Assert.That(response.Id, Is.Not.Null);
        Assert.That(response.Id, Is.GreaterThan(0));
        var updateRequest = new UpdateEventSubscriptionsRequest
        {
            // The response is passing a long to a SubscriptionId that is of type int.
            // Both the API and SDK should be updated to use a consistent type.
            SubscriptionId = (int)response.Id,
            Body = EventSubscriptionFactory.Create(false)
        }; 
        
        // There is no response body for update, so we just ensure no exception was thrown
        Assert.DoesNotThrowAsync(async () =>
        {
            await client.Notifications.EventSubscriptions.UpdateAsync(updateRequest);
        });
    }
}
