using NUnit.Framework;
using Payroc;
using Payroc.Notifications.EventSubscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Notifications.EventSubscriptions;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "enabled": true,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress",
                  "type": "webhook"
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/event-subscriptions/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Notifications.EventSubscriptions.UpdateAsync(
                new UpdateEventSubscriptionsRequest
                {
                    SubscriptionId = 1,
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
                                        Uri = "https://my-server/notification/endpoint",
                                        Secret = "aBcD1234eFgH5678iJkL9012mNoP3456",
                                        SupportEmailAddress = "supportEmailAddress",
                                    }
                                )
                            ),
                        },
                        Metadata = new Dictionary<string, object>()
                        {
                            { "yourCustomField", "abc123" },
                        },
                    },
                }
            )
        );
    }
}
