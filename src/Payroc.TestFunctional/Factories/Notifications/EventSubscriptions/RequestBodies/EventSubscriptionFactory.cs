namespace Payroc.TestFunctional.Factories.Notifications.EventSubscriptions.RequestBodies;

public class EventSubscriptionFactory
{
    public static EventSubscription Create(bool enabled = true) => new()
    {
        Enabled = enabled,
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
    };
}