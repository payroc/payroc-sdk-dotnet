using NUnit.Framework;
using Payroc.EventSubscriptions;

namespace Payroc.Test.Unit.MockServer;

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
