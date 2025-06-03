using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class PricingTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "country": "US",
              "base": {
                "addressVerification": 5,
                "annualFee": {
                  "billInMonth": "june",
                  "amount": 100
                },
                "regulatoryAssistanceProgram": 15,
                "pciNonCompliance": 7495,
                "merchantAdvantage": 10,
                "platinumSecurity": {
                  "amount": 1295,
                  "billingFrequency": "monthly"
                },
                "maintenance": 1995,
                "minimum": 100,
                "voiceAuthorization": 95,
                "chargeback": 2500,
                "retrieval": 1500,
                "batch": 5,
                "earlyTermination": 57500
              },
              "processor": {
                "card": {
                  "fees": {
                    "mastercardVisaDiscover": {
                      "volume": 1.25
                    },
                    "amex": {
                      "volume": 1.25,
                      "transaction": 1,
                      "type": "optBlue"
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
                  },
                  "planType": "interchangePlus"
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
                    "discountRateUnder10000": 1.25,
                    "discountRateAbove10000": 1.25
                  }
                }
              },
              "gateway": {
                "fees": {
                  "monthly": 1,
                  "setup": 1,
                  "perTransaction": 1,
                  "perDeviceMonthly": 1
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "version": "5.0"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765/pricing")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingAccounts.PricingAsync(
            new PricingProcessingAccountsRequest { ProcessingAccountId = "38765" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingProcessingAccountsResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
