using NUnit.Framework;
using Payroc;
using Payroc.PaymentLinks.SharingEvents;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentLinks.SharingEvents;

[TestFixture]
public class ShareTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "sharingMethod": "email",
              "merchantCopy": true,
              "message": "Dear Sarah,\n\nYour insurance is expiring this month.\nPlease, pay the renewal fee by the end of the month to renew it.\n",
              "recipients": [
                {
                  "name": "Sarah Hazel Hopper",
                  "email": "sarah.hopper@example.com"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "sharingMethod": "email",
              "message": "Dear Sarah,\n\nYou can pay for your order via the link below.\n",
              "recipients": [
                {
                  "name": "Sarah Hazel Hopper",
                  "email": "sarah.hopper@example.com"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/payment-links/JZURRJBUPS/sharing-events")
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

        var response = await Client.PaymentLinks.SharingEvents.ShareAsync(
            new ShareSharingEventsRequest
            {
                PaymentLinkId = "JZURRJBUPS",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PaymentLinkEmailShareEvent
                {
                    SharingMethod = PaymentLinkEmailShareEventSharingMethod.Email,
                    MerchantCopy = true,
                    Message =
                        "Dear Sarah,\n\nYour insurance is expiring this month.\nPlease, pay the renewal fee by the end of the month to renew it.\n",
                    Recipients = new List<PaymentLinkEmailRecipient>()
                    {
                        new PaymentLinkEmailRecipient
                        {
                            Name = "Sarah Hazel Hopper",
                            Email = "sarah.hopper@example.com",
                        },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
