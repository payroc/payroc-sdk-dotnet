using NUnit.Framework;
using Payroc.Boarding.PricingIntents;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.PricingIntents;

[TestFixture]
public class DeleteTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/pricing-intents/5")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Boarding.PricingIntents.DeleteAsync(
                new DeletePricingIntentsRequest { PricingIntentId = "5" }
            )
        );
    }
}
