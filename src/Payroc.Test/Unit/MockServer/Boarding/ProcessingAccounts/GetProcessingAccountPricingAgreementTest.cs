using NUnit.Framework;
using OneOf;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class GetProcessingAccountPricingAgreementTest : BaseMockServerTest
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
                  "billInMonth": "december",
                  "amount": 9900
                },
                "regulatoryAssistanceProgram": 15,
                "pciNonCompliance": 4995,
                "merchantAdvantage": 10,
                "platinumSecurity": {
                  "amount": 1295,
                  "billingFrequency": "monthly"
                },
                "maintenance": 500,
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
                  "monthly": 0,
                  "setup": 0,
                  "perTransaction": 0,
                  "perDeviceMonthly": 0
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ]
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

        var response =
            await Client.Boarding.ProcessingAccounts.GetProcessingAccountPricingAgreementAsync(
                new GetProcessingAccountPricingAgreementProcessingAccountsRequest
                {
                    ProcessingAccountId = "38765",
                }
            );
        Assert.That(
            response.Value,
            Is.EqualTo(
                    JsonUtils
                        .Deserialize<OneOf<PricingAgreementUs40, PricingAgreementUs50>>(
                            mockResponse
                        )
                        .Value
                )
                .UsingDefaults()
        );
    }
}
