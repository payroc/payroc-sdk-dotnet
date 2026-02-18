using NUnit.Framework;
using Payroc;
using Payroc.Notifications.EventSubscriptions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Notifications.EventSubscriptions;

[TestFixture]
public class PartiallyUpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_3()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_4()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_5()
    {
        const string requestJson = """
            [
              {
                "op": "move",
                "from": "from",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_6()
    {
        const string requestJson = """
            [
              {
                "op": "copy",
                "from": "from",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_7()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_8()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "move",
                "from": "from",
                "path": "path"
              },
              {
                "op": "copy",
                "from": "from",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              }
            ]
            """;

        const string mockResponse = """
            {
              "enabled": false,
              "eventTypes": [
                "processingAccount.status.changed"
              ],
              "notifications": [
                {
                  "type": "webhook",
                  "uri": "https://my-server/notification/endpoint",
                  "secret": "aBcD1234eFgH5678iJkL9012mNoP3456",
                  "supportEmailAddress": "supportEmailAddress"
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
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
