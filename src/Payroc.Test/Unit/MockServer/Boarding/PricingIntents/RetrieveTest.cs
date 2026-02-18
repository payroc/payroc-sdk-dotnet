using NUnit.Framework;
using Payroc.Boarding.PricingIntents;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.PricingIntents;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "country": "US",
              "version": "5.0",
              "base": {
                "addressVerification": 5,
                "annualFee": {
                  "billInMonth": "june",
                  "amount": 100
                },
                "regulatoryAssistanceProgram": 15,
                "pciNonCompliance": 4995,
                "merchantAdvantage": 10,
                "platinumSecurity": {
                  "billingFrequency": "monthly"
                },
                "maintenance": 500,
                "minimum": 100,
                "voiceAuthorization": 95,
                "chargeback": 2500,
                "retrieval": 1500,
                "batch": 1000,
                "earlyTermination": 57500
              },
              "processor": {
                "card": {
                  "planType": "interchangePlus",
                  "fees": {
                    "mastercardVisaDiscover": {
                      "volume": 1.25
                    },
                    "amex": {
                      "type": "optBlue",
                      "volume": 1.25,
                      "transaction": 1
                    },
                    "pinDebit": {
                      "additionalDiscount": 1.25,
                      "transaction": 1,
                      "monthlyAccess": 1
                    },
                    "enhancedInterchange": {
                      "enrollment": 1,
                      "creditToMerchant": 1.25
                    }
                  }
                },
                "ach": {
                  "fees": {
                    "transaction": 50,
                    "batch": 1000,
                    "returns": 400,
                    "unauthorizedReturn": 1999,
                    "statement": 800,
                    "monthlyMinimum": 20000,
                    "accountVerification": 100,
                    "discountRateUnder10000": 5.25,
                    "discountRateAbove10000": 10
                  }
                }
              },
              "gateway": {
                "fees": {
                  "monthly": 1000,
                  "setup": 25000,
                  "perTransaction": 0,
                  "perDeviceMonthly": 0
                }
              },
              "services": [
                {
                  "name": "hardwareAdvantagePlan",
                  "enabled": true
                }
              ],
              "key": "string",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/pricing-intents/5").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.PricingIntents.RetrieveAsync(
            new RetrievePricingIntentsRequest { PricingIntentId = "5" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
