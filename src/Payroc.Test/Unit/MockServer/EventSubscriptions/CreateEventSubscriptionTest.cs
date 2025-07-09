using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.EventSubscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.EventSubscriptions;

[TestFixture]
public class CreateEventSubscriptionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
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

        const string mockResponse = """
            {
              "id": 2565435189324,
              "enabled": true,
              "status": "registered",
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
                    .WithPath("/event-subscriptions")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EventSubscriptions.CreateEventSubscriptionAsync(
            new CreateEventSubscriptionRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
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
                    Metadata = new Dictionary<string, object>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<EventSubscription>(mockResponse)).UsingDefaults()
        );
    }
}
