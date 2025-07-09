using NUnit.Framework;
using Payroc.EventSubscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.EventSubscriptions;

[TestFixture]
public class DeleteEventSubscriptionTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/event-subscriptions/1")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EventSubscriptions.DeleteEventSubscriptionAsync(
                new DeleteEventSubscriptionRequest { SubscriptionId = 1 }
            )
        );
    }
}
