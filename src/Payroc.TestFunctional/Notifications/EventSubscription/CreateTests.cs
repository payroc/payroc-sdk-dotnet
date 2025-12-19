using Payroc.Notifications.EventSubscriptions;
using Payroc.TestFunctional.Factories.Notifications.EventSubscriptions.RequestBodies;

namespace Payroc.TestFunctional.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
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

        Assert.That(response.Id, Is.GreaterThan(0));
        Assert.That(response.Status, Is.Not.Null);
    }
}
