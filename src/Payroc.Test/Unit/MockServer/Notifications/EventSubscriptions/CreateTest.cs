using NUnit.Framework;
using Payroc;
using Payroc.Notifications.EventSubscriptions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Notifications.EventSubscriptions;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "enabled": true,
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

        const string mockResponse = """
            {
              "enabled": true,
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

        var response = await Client.Notifications.EventSubscriptions.CreateAsync(
            new CreateEventSubscriptionsRequest
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
                    Metadata = new Dictionary<string, object?>()
                    {
                        { "yourCustomField", "abc123" },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "enabled": true,
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
                "responsiblePerson": "Jane Doe"
              }
            }
            """;

        const string mockResponse = """
            {
              "enabled": true,
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

        var response = await Client.Notifications.EventSubscriptions.CreateAsync(
            new CreateEventSubscriptionsRequest
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
                    Metadata = new Dictionary<string, object?>()
                    {
                        { "responsiblePerson", "Jane Doe" },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_3()
    {
        const string requestJson = """
            {
              "enabled": true,
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

        const string mockResponse = """
            {
              "enabled": true,
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

        var response = await Client.Notifications.EventSubscriptions.CreateAsync(
            new CreateEventSubscriptionsRequest
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
                    Metadata = new Dictionary<string, object?>()
                    {
                        { "yourCustomField", "abc123" },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_4()
    {
        const string requestJson = """
            {
              "enabled": true,
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

        const string mockResponse = """
            {
              "enabled": true,
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
                "responsiblePerson": "Jane Doe"
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

        var response = await Client.Notifications.EventSubscriptions.CreateAsync(
            new CreateEventSubscriptionsRequest
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
                    Metadata = new Dictionary<string, object?>()
                    {
                        { "yourCustomField", "abc123" },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
