using NUnit.Framework;
using Payroc;
using Payroc.PaymentFeatures.Cards;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentFeatures.Cards;

[TestFixture]
public class LookupBinTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "processingTerminalId": "1234001",
              "card": {
                "type": "card",
                "cardDetails": {
                  "entryMethod": "raw",
                  "device": {
                    "model": "bbposChp",
                    "serialNumber": "1850010868"
                  },
                  "rawData": "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF"
                }
              }
            }
            """;

        const string mockResponse = """
            {
              "type": "MASTERCARD",
              "cardNumber": "453985******7062",
              "country": "US",
              "currency": "USD",
              "debit": false,
              "surcharging": {
                "allowed": true,
                "amount": 87,
                "percentage": 3,
                "disclosure": "A 3% surcharge is applied to cover processing fees."
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/cards/bin-lookup")
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

        var response = await Client.PaymentFeatures.Cards.LookupBinAsync(
            new BinLookup
            {
                ProcessingTerminalId = "1234001",
                Card = new BinLookupCard(
                    new Payroc.PaymentFeatures.Cards.BinLookupCard.Card(
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
        JsonAssert.AreEqual(response, mockResponse);
    }
}
