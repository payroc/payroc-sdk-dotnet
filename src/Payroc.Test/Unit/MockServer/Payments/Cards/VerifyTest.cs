using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.Cards;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.Cards;

[TestFixture]
public class VerifyTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "card": {
                "cardDetails": {
                  "device": {
                    "model": "bbposChp",
                    "serialNumber": "1850010868"
                  },
                  "rawData": "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                  "entryMethod": "raw"
                },
                "type": "card"
              }
            }
            """;

        const string mockResponse = """
            {
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "verified": true,
              "dateTime": "2024-07-02T15:30:00.000Z",
              "responseCode": "A",
              "responseMessage": "Refer to Card Issuer",
              "processorResponseCode": "A"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/cards/verify")
                    .WithHeader("Idempotency-Key", "Idempotency-Key")
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

        var response = await Client.Payments.Cards.VerifyAsync(
            new CardVerificationRequest
            {
                IdempotencyKey = "Idempotency-Key",
                ProcessingTerminalId = "1234001",
                Operator = "Jane",
                Card = new CardVerificationRequestCard(
                    new CardVerificationRequestCard.Card(
                        new CardPayload
                        {
                            CardDetails = new CardPayloadCardDetails(
                                new CardPayloadCardDetails.Raw(
                                    new RawCardDetails
                                    {
                                        Device = new Device
                                        {
                                            Model = DeviceModel.BbposChp,
                                            SerialNumber = "1850010868",
                                        },
                                        RawData =
                                            "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                                    }
                                )
                            ),
                        }
                    )
                ),
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CardVerificationResult>(mockResponse)).UsingDefaults()
        );
    }
}
