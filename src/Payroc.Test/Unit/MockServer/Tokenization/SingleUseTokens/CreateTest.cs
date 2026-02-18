using NUnit.Framework;
using Payroc;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;
using Payroc.Tokenization.SingleUseTokens;

namespace Payroc.Test.Unit.MockServer.Tokenization.SingleUseTokens;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "channel": "web",
              "operator": "Jane",
              "source": {
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
              "source": {
                "type": "card",
                "cardholderName": "Sarah Hazel Hopper",
                "cardNumber": "4539858876047062",
                "expiryDate": "1230",
                "cardType": "cardType",
                "currency": "AED",
                "debit": true,
                "surcharging": {
                  "allowed": true,
                  "amount": 87,
                  "percentage": 3,
                  "disclosure": "A 3% surcharge is applied to cover processing fees."
                }
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/single-use-tokens")
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

        var response = await Client.Tokenization.SingleUseTokens.CreateAsync(
            new SingleUseTokenRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Channel = SingleUseTokenRequestChannel.Web,
                Operator = "Jane",
                Source = new SingleUseTokenRequestSource(
                    new Payroc.Tokenization.SingleUseTokens.SingleUseTokenRequestSource.Card(
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
