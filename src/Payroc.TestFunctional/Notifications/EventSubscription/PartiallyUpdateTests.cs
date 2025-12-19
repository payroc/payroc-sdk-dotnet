using Payroc.Notifications.EventSubscriptions;
using Payroc.TestFunctional.Factories.Notifications.EventSubscriptions.RequestBodies;

namespace Payroc.TestFunctional.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class PartiallyUpdateTests
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
        var partiallyUpdateRequest = new PartiallyUpdateEventSubscriptionsRequest
        {
            // The response is passing a long to a SubscriptionId that is of type int.
            // Both the API and SDK should be updated to use a consistent type. 
            SubscriptionId = (int)response.Id,
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = new List<PatchDocument>
            {
                new PatchDocument(
                    new PatchDocument.Replace(
                        new PatchReplace
                        {
                            Path = "/notifications/0/supportEmailAddress",
                            Value = "updated-email@email.com"
                        }))
            }
        };
        
        var partiallyUpdateResponse = await client.Notifications.EventSubscriptions.PartiallyUpdateAsync(partiallyUpdateRequest);
        
        Assert.That(partiallyUpdateResponse.Status, Is.Not.Null);
        Assert.That(partiallyUpdateResponse.Id, Is.GreaterThan(0));
    }
}
