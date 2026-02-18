using NUnit.Framework;
using Payroc;
using Payroc.PaymentFeatures.Cards;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentFeatures.Cards;

[TestFixture]
public class RetrieveFxRatesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "channel": "web",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "baseAmount": 10000,
              "baseCurrency": "USD",
              "paymentMethod": {
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
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "baseAmount": 10000,
              "baseCurrency": "EUR",
              "inquiryResult": {
                "dccOffered": true,
                "causeOfRejection": "Service unavailable"
              },
              "dccOffer": {
                "offerReference": "DCC123456789",
                "fxAmount": 16125,
                "fxCurrency": "JPY",
                "fxRate": 161.2542,
                "markup": 3
              },
              "cardInfo": {
                "type": "MASTERCARD",
                "cardNumber": "453985******7062",
                "country": "country",
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
                    .WithPath("/fx-rates")
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

        var response = await Client.PaymentFeatures.Cards.RetrieveFxRatesAsync(
            new FxRateInquiry
            {
                Channel = FxRateInquiryChannel.Web,
                ProcessingTerminalId = "1234001",
                Operator = "Jane",
                BaseAmount = 10000,
                BaseCurrency = Currency.Usd,
                PaymentMethod = new FxRateInquiryPaymentMethod(
                    new Payroc.PaymentFeatures.Cards.FxRateInquiryPaymentMethod.Card(
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
