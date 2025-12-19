using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;
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
              "paymentMethod": {
                "accountType": "checking",
                "cardDetails": {
                  "downgradeTo": "keyed",
                  "device": {
                    "model": "bbposChp",
                    "serialNumber": "1850010868",
                    "firmwareVersion": "v1.2.3",
                    "config": {
                      "quickChip": false
                    }
                  },
                  "rawData": "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                  "cardholderSignature": "a1b1c012345678a000b000c0012345d0e0f010g10061a031i001j071k0a1b0c1d0e1234567890120f1g0h1i0j1k0a1b0123451c012d0e1f0g1h0i1j123k1a1b1c1d1e1f1g123h1i1j1k1a1b1c1d1e1f1g123h123i1j123k12340a120a12345b012c0123012d0d1e0f1g0h1i123j123k10000",
                  "entryMethod": "raw"
                },
                "type": "card"
              },
              "token": "fa2e9e51bc5265a33a5ca41449524d53d1def596ffd8c0904f222183a71a65cdb58835120a65196a48a6375abc4deafe2b7e948689ab9d6aba919e860f32e247",
              "expiresAt": "2024-08-05T17:50:05.000Z",
              "source": {
                "cardholderName": "Sarah Hazel Hopper",
                "cardNumber": "4539858876047062",
                "expiryDate": "1225",
                "cardType": "cardType",
                "currency": "AED",
                "debit": true,
                "surcharging": {
                  "allowed": true,
                  "amount": 87,
                  "percentage": 3,
                  "disclosure": "A 3% surcharge is applied to cover processing fees."
                },
                "type": "card"
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<SingleUseToken>(mockResponse)).UsingDefaults()
        );
    }
}
