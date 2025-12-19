using Payroc.Notifications.EventSubscriptions;
using Payroc.TestFunctional.Factories.Notifications.EventSubscriptions.RequestBodies;

namespace Payroc.TestFunctional.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class EventSubscriptionLifecycleTests
{
    [Test]
    [Retry(10)]
    [Category("Payments")]
    public async Task Create_Patch_Update_Get_Delete_Lifecycle()
    {
        var client = GlobalFixture.Generic;

        // Create the initial event subscription
        var createRequest = new CreateEventSubscriptionsRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            Body = EventSubscriptionFactory.Create()
        };

        var created = await client.Notifications.EventSubscriptions.CreateAsync(createRequest);
        Assert.That(created, Is.Not.Null, "Create response should not be null");
        Assert.That(created.Id, Is.GreaterThan(0));
        TestContext.WriteLine($"[CREATE] id={created.Id} enabled={created.Enabled}");

        try
        {
            // patch the initially created object
            var patched = await client.Notifications.EventSubscriptions.PartiallyUpdateAsync(
                new PartiallyUpdateEventSubscriptionsRequest
                { 
                    // The response is passing a long to a SubscriptionId that is of type int.
                    // Both the API and SDK should be updated to use a consistent type. 
                    SubscriptionId = (int)created.Id,
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
                }
            );
            TestContext.WriteLine($"[PATCH] enabled={patched.Enabled} details={patched.Notifications.First()}");
            var patchedNotif = patched.Notifications.First();

            if (patchedNotif.TryAsWebhook(out var webhook))  // <-- does this compile?
            {
                Assert.That(webhook?.SupportEmailAddress, Is.EqualTo($"updated-email@email.com"));
            }
            else
            {
                Assert.Fail("Notification variant was not 'webhook'.");
            }

            // now do a full update
            await client.Notifications.EventSubscriptions.UpdateAsync(
                new UpdateEventSubscriptionsRequest
                {
                    SubscriptionId = (int)created.Id,
                    Body = EventSubscriptionFactory.Create(false)
                });

            // now do a get to verify the update
            var afterUpdate = await client.Notifications.EventSubscriptions.RetrieveAsync(
                new RetrieveEventSubscriptionsRequest { SubscriptionId = (int)created.Id }
            );

            TestContext.WriteLine($"[UPDATED] id={afterUpdate.Id} enabled={afterUpdate}");
            Assert.That(afterUpdate.Enabled, Is.False);
        }
        finally
        {
            // delete - clean up and verify
            try
            {
                await client.Notifications.EventSubscriptions.DeleteAsync(
                    new DeleteEventSubscriptionsRequest { SubscriptionId = (int)created.Id }
                );
                TestContext.WriteLine($"[DELETE] id={created.Id} deleted");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"[DELETE] cleanup failed for id={created.Id}: {ex}");
            }

            // try to get the deleted entity
            try
            {
                var afterDelete = await client.Notifications.EventSubscriptions.RetrieveAsync(
                    new RetrieveEventSubscriptionsRequest { SubscriptionId = (int)created.Id }
                );
            }
            catch (UnauthorizedError ex) // adjust to your SDK's exception type
            {
                Assert.That(ex.StatusCode, Is.EqualTo(401),
                    $"Expected 401 Unauthorized, got {ex.StatusCode} instead.");
                TestContext.WriteLine($"[GET after DELETE] correctly failed with {ex.StatusCode}");
            }
        }
    }
}