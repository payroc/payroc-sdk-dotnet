using Payroc.Notifications.EventSubscriptions;

namespace Payroc.TestFunctional.EventSubscriptions;

[TestFixture, Category("Notifications.EventSubscriptions")]
[Parallelizable(ParallelScope.Fixtures)]
public class EventSubscriptionLifecycleTests
{
    private static string NewKey() => Guid.NewGuid().ToString();

    [Test]
    [Retry(10)]
    [Category("Payments")]
    [Ignore("Flakey test - works sometimes, not others")]
    public async Task Create_Patch_Update_Get_Delete_Lifecycle()
    {
        var client = GlobalFixture.Generic;

        // Create the initial event subscription
        var createRequest = new CreateEventSubscriptionsRequest
        {
            IdempotencyKey = NewKey(),
            Body = new EventSubscription
            {
                Enabled = true,
                EventTypes = new List<string>() { "processingAccount.status.changed" },
                Notifications = new List<Notification>()
                {
                    new Notification(
                        new Notification.Webhook(
                            new Webhook
                            {
                                Uri = "https://test-webhook-url/endpoint",
                                Secret = "1234567890123456",
                                SupportEmailAddress = "support@email.com"
                            }
                        )),
                },
                Metadata = new() { { "customMetaData", "customValue123" } },
            }
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
                    SubscriptionId = (int)created.Id,
                    IdempotencyKey = NewKey(),
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
                    Body = new EventSubscription
                    {
                        Enabled = false,
                        EventTypes = new List<string>() { "processingAccount.status.changed" },
                        Notifications = new List<Notification>()
                        {
                            new Notification(
                                new Notification.Webhook(
                                    new Webhook
                                    {
                                        Uri = "https://updated-url/endpoint",
                                        Secret = "1234567890123456",
                                        SupportEmailAddress = "updated-email2@email.com"
                                    })
                            )
                        },
                        Metadata = new() { { "updatedMetaData", "customValue123" } },
                    }
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