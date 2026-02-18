using NUnit.Framework;
using Payroc;
using Payroc.Boarding.PricingIntents;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.PricingIntents;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "country": "US",
              "version": "5.0",
              "base": {
                "addressVerification": 5,
                "annualFee": {
                  "billInMonth": "june",
                  "amount": 9900
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
                "batch": 1500,
                "earlyTermination": 57500
              },
              "processor": {
                "card": {
                  "planType": "interchangePlus",
                  "fees": {
                    "mastercardVisaDiscover": {}
                  }
                },
                "ach": {
                  "fees": {
                    "transaction": 50,
                    "batch": 5,
                    "returns": 400,
                    "unauthorizedReturn": 1999,
                    "statement": 800,
                    "monthlyMinimum": 20000,
                    "accountVerification": 10,
                    "discountRateUnder10000": 5.25,
                    "discountRateAbove10000": 10
                  }
                }
              },
              "gateway": {
                "fees": {
                  "monthly": 2000,
                  "setup": 5000,
                  "perTransaction": 2000,
                  "perDeviceMonthly": 10
                }
              },
              "services": [
                {
                  "name": "hardwareAdvantagePlan",
                  "enabled": true
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/pricing-intents/5")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Boarding.PricingIntents.UpdateAsync(
                new UpdatePricingIntentsRequest
                {
                    PricingIntentId = "5",
                    Body = new PricingIntent50
                    {
                        Country = PricingAgreementUs50Country.Us,
                        Version = PricingAgreementUs50Version.Five0,
                        Base = new BaseUs
                        {
                            AddressVerification = 5,
                            AnnualFee = new BaseUsAnnualFee
                            {
                                BillInMonth = BaseUsAnnualFeeBillInMonth.June,
                                Amount = 9900,
                            },
                            RegulatoryAssistanceProgram = 15,
                            PciNonCompliance = 4995,
                            MerchantAdvantage = 10,
                            PlatinumSecurity = new BaseUsPlatinumSecurity(
                                new BaseUsPlatinumSecurity.Monthly(new BaseUsMonthly())
                            ),
                            Maintenance = 500,
                            Minimum = 100,
                            VoiceAuthorization = 95,
                            Chargeback = 2500,
                            Retrieval = 1500,
                            Batch = 1500,
                            EarlyTermination = 57500,
                        },
                        Processor = new PricingAgreementUs50Processor
                        {
                            Card = new PricingAgreementUs50ProcessorCard(
                                new PricingAgreementUs50ProcessorCard.InterchangePlus(
                                    new InterchangePlus
                                    {
                                        Fees = new InterchangePlusFees
                                        {
                                            MastercardVisaDiscover = new ProcessorFee(),
                                        },
                                    }
                                )
                            ),
                            Ach = new Ach
                            {
                                Fees = new AchFees
                                {
                                    Transaction = 50,
                                    Batch = 5,
                                    Returns = 400,
                                    UnauthorizedReturn = 1999,
                                    Statement = 800,
                                    MonthlyMinimum = 20000,
                                    AccountVerification = 10,
                                    DiscountRateUnder10000 = 5.25,
                                    DiscountRateAbove10000 = 10,
                                },
                            },
                        },
                        Gateway = new GatewayUs50
                        {
                            Fees = new GatewayUs50Fees
                            {
                                Monthly = 2000,
                                Setup = 5000,
                                PerTransaction = 2000,
                                PerDeviceMonthly = 10,
                            },
                        },
                        Services = new List<ServiceUs50>()
                        {
                            new ServiceUs50(
                                new ServiceUs50.HardwareAdvantagePlan(
                                    new HardwareAdvantagePlan { Enabled = true }
                                )
                            ),
                        },
                        Key = "Your-Unique-Identifier",
                        Metadata = new Dictionary<string, string>()
                        {
                            { "yourCustomField", "abc123" },
                        },
                    },
                }
            )
        );
    }
}
