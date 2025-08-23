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
                    .WithPath(
                        "/processing-terminals/processingTerminalId/payment-plans/paymentPlanId"
                    )
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Payments.PaymentPlans.DeleteAsync(
                new DeletePaymentPlansRequest
                {
                    ProcessingTerminalId = "processingTerminalId",
                    PaymentPlanId = "paymentPlanId",
                }
            )
        );
    }
}
