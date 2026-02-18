using NUnit.Framework;
using Payroc;
using Payroc.PaymentFeatures.Cards;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentFeatures.Cards;

[TestFixture]
public class VerifyCardTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "processingTerminalId": "1234001",
              "operator": "Jane",
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
              "operator": "Jane",
              "processingTerminalId": "1234001",
              "card": {
                "type": "Visa Credit",
                "entryMethod": "keyed",
                "cardholderName": "Sarah Hazel Hopper",
                "cardholderSignature": "a1b1c012345678a000b000c0012345d0e0f010g10061a031i001j071k0a1b0c1d0e1234567890120f1g0h1i0j1k0a1b0123451c012d0e1f0g1h0i1j123k1a1b1c1d1e1f1g123h1i1j1k1a1b1c1d1e1f1g123h123i1j123k12340a120a12345b012c0123012d0d1e0f1g0h1i123j123k10000",
                "cardNumber": "453985******7062",
                "expiryDate": "1230",
                "secureToken": {
                  "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                  "customerName": "Sarah Hazel Hopper",
                  "token": "296753123456",
                  "status": "notValidated",
                  "link": {
                    "rel": "previous",
                    "method": "get",
                    "href": "<uri>"
                  }
                },
                "securityChecks": {
                  "cvvResult": "M",
                  "avsResult": "Y"
                },
                "emvTags": [
                  {
                    "hex": "9F36",
                    "value": "001234"
                  },
                  {
                    "hex": "5F2A",
                    "value": "0840"
                  }
                ],
                "balances": [
                  {
                    "benefitCategory": "cash",
                    "amount": 50000,
                    "currency": "USD"
                  },
                  {
                    "benefitCategory": "foodStamp",
                    "amount": 10000,
                    "currency": "USD"
                  }
                ]
              },
              "verified": true,
              "transactionResult": {
                "type": "sale",
                "ebtType": "cashPurchase",
                "status": "ready",
                "approvalCode": "approvalCode",
                "authorizedAmount": 1000000,
                "currency": "AED",
                "responseCode": "A",
                "responseMessage": "APPROVAL",
                "processorResponseCode": "00",
                "cardSchemeReferenceId": "cardSchemeReferenceId"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/cards/verify")
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

        var response = await Client.PaymentFeatures.Cards.VerifyCardAsync(
            new CardVerificationRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                ProcessingTerminalId = "1234001",
                Operator = "Jane",
                Card = new CardVerificationRequestCard(
                    new Payroc.PaymentFeatures.Cards.CardVerificationRequestCard.Card(
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
