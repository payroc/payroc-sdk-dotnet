using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Notifications.EventSubscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Notifications.EventSubscriptions;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
                    .WithPath("/event-subscriptions/1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Notifications.EventSubscriptions.RetrieveAsync(
            new RetrieveEventSubscriptionsRequest { SubscriptionId = 1 }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<EventSubscription>(mockResponse)).UsingDefaults()
        );
    }
}
