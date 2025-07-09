using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.EventSubscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.EventSubscriptions;

[TestFixture]
public class ListEventSubscriptionsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "limit": 10,
              "count": 2,
              "hasMore": false,
              "links": [
                {
                  "rel": "next",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/event-subscriptions?after=10&limit=10"
                }
              ],
              "data": [
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
                },
                {
                  "id": 2565435189325,
                  "enabled": true,
                  "status": "registered",
                  "eventTypes": [
                    "processingAccount.riskStatus.changed"
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
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/event-subscriptions")
                    .WithParam("event", "processingAccount.status.changed")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EventSubscriptions.ListEventSubscriptionsAsync(
            new ListEventSubscriptionsRequest { Event = "processingAccount.status.changed" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaginatedEventSubscriptions>(mockResponse))
                .UsingDefaults()
        );
    }
}
