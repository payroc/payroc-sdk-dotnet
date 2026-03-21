using Payroc.Notifications.EventSubscriptions;
using Payroc.TestCommon.Factories.Notifications.EventSubscriptions.RequestBodies;

namespace Payroc.TestCommon.Tests.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    [Ignore("API Key config issue - need event supported")]
    public async Task EventSubscription_Create_Success()
    {
        var client = GlobalFixture.Payments;
        var request = new CreateEventSubscriptionsRequest()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = EventSubscriptionFactory.Create()
        };

        var response = await client.Notifications.EventSubscriptions.CreateAsync(request);

        Assert.That(response.Id, Is.GreaterThan(0));
        Assert.That(response.Status, Is.Not.Null);
    }
}
