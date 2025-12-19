using Payroc.Notifications.EventSubscriptions;
using Payroc.TestFunctional.Factories.Notifications.EventSubscriptions.RequestBodies;

namespace Payroc.TestFunctional.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
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
        _ = await client.Notifications.EventSubscriptions.CreateAsync(request);
        _ = await client.Notifications.EventSubscriptions.CreateAsync(request);
        var listRequest = new ListEventSubscriptionsRequest()
        {
            Event = response.EventTypes.First()
        };
        
        var listResponse = await client.Notifications.EventSubscriptions.ListAsync(listRequest);
        
        Assert.That(listResponse.CurrentPage.Items.Count, Is.GreaterThan(2));
    }
}
