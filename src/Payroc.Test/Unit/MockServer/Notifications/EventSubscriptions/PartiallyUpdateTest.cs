using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Notifications.EventSubscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Notifications.EventSubscriptions;

[TestFixture]
public class PartiallyUpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

        const string mockResponse = """
            {
              "id": 2565435189324,
              "enabled": false,
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
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Notifications.EventSubscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateEventSubscriptionsRequest
            {
                SubscriptionId = 1,
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<EventSubscription>(mockResponse)).UsingDefaults()
        );
    }
}
