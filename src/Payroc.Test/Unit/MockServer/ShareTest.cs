using global::System.Threading.Tasks;
using NUnit.Framework;
using OneOf;
using Payroc;
using Payroc.Core;
using Payroc.Payments.PaymentLinks.SharingEvents;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class ShareTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
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
              "sharingEventId": "GTZH5WVXK9",
              "dateTime": "2024-07-02T15:30:00.000Z",
              "merchantCopy": true,
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

        var response = await Client.Payments.PaymentLinks.SharingEvents.ShareAsync(
            new ShareSharingEventsRequest
            {
                PaymentLinkId = "JZURRJBUPS",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PaymentLinkEmailShareEvent
                {
                    SharingMethod = "email",
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
        Assert.That(
            response.Value,
            Is.EqualTo(JsonUtils.Deserialize<OneOf<PaymentLinkEmailShareEvent>>(mockResponse).Value)
                .UsingDefaults()
        );
    }
}
