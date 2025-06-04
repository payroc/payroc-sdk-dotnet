using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.PricingIntents;
using Payroc.Core;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class PatchTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

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
                "batch": 1000,
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
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "id": "5",
              "createdDate": "2024-07-02T09:00:00.000Z",
              "lastUpdatedDate": "2024-07-02T09:00:00.000Z",
              "status": "pendingReview",
              "key": "string",
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
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.PricingIntents.PatchAsync(
            new PatchPricingIntentsRequest
            {
                PricingIntentId = "5",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "from": "from",
                "path": "path",
                "op": "move"
              },
              {
                "from": "from",
                "path": "path",
                "op": "copy"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

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
                "batch": 1000,
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
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "id": "5",
              "createdDate": "2024-07-02T09:00:00.000Z",
              "lastUpdatedDate": "2024-07-02T09:00:00.000Z",
              "status": "pendingReview",
              "key": "string",
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
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.PricingIntents.PatchAsync(
            new PatchPricingIntentsRequest
            {
                PricingIntentId = "5",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }
}
