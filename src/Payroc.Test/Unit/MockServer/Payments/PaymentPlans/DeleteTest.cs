using NUnit.Framework;
using Payroc.Payments.PaymentPlans;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.PaymentPlans;

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
                    .WithPath("/processing-terminals/1234001/payment-plans/PlanRef8765")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Payments.PaymentPlans.DeleteAsync(
                new DeletePaymentPlansRequest
                {
                    ProcessingTerminalId = "1234001",
                    PaymentPlanId = "PlanRef8765",
                }
            )
        );
    }
}
