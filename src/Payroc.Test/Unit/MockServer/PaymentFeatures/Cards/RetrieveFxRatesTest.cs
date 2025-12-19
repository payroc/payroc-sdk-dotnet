using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.PaymentFeatures.Cards;
using Payroc.Test.Unit.MockServer;

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
              "baseAmount": 10000,
              "baseCurrency": "EUR",
              "inquiryResult": {
                "dccOffered": true,
                "causeOfRejection": "Service unavailable"
              },
              "dccOffer": {
                "accepted": true,
                "offerReference": "DCC123456789",
                "fxAmount": 16125,
                "fxCurrency": "JPY",
                "fxCurrencyCode": "392",
                "fxCurrencyExponent": 0,
                "fxRate": 161.2542,
                "markup": 3,
                "markupText": "3.5% mark-up applied.",
                "provider": "FEXCO",
                "source": "REUTERS WHOLESALE INTERBANK"
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<FxRate>(mockResponse)).UsingDefaults()
        );
    }
}
