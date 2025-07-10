using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.PricingIntents;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.PricingIntents;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
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
                  "fees": {
                    "mastercardVisaDiscover": {}
                  },
                  "planType": "interchangePlus"
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
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
                  "fees": {
                    "mastercardVisaDiscover": {
                      "qualifiedRate": {},
                      "midQualRate": {},
                      "nonQualRate": {}
                    }
                  },
                  "planType": "interchangePlusPlus"
                },
                "ach": {
                  "fees": {
                    "transaction": 50,
                    "batch": 5,
                    "returns": 400,
                    "unauthorizedReturn": 500,
                    "statement": 250,
                    "monthlyMinimum": 500,
                    "accountVerification": 10,
                    "discountRateUnder10000": 0.5,
                    "discountRateAbove10000": 1
                  }
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                            new PricingAgreementUs50ProcessorCard.InterchangePlusPlus(
                                new InterchangePlusPlus
                                {
                                    Fees = new InterchangePlusPlusFees
                                    {
                                        MastercardVisaDiscover = new QualRates
                                        {
                                            QualifiedRate = new ProcessorFee(),
                                            MidQualRate = new ProcessorFee(),
                                            NonQualRate = new ProcessorFee(),
                                        },
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
                                UnauthorizedReturn = 500,
                                Statement = 250,
                                MonthlyMinimum = 500,
                                AccountVerification = 10,
                                DiscountRateUnder10000 = 0.5,
                                DiscountRateAbove10000 = 1,
                            },
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_3()
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
                  "fees": {
                    "mastercardVisaDiscover": {
                      "qualifiedRate": {},
                      "midQualRate": {},
                      "nonQualRate": {}
                    }
                  },
                  "planType": "tiered3"
                },
                "ach": {
                  "fees": {
                    "transaction": 50,
                    "batch": 5,
                    "returns": 400,
                    "unauthorizedReturn": 500,
                    "statement": 250,
                    "monthlyMinimum": 500,
                    "accountVerification": 10,
                    "discountRateUnder10000": 0.5,
                    "discountRateAbove10000": 1
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
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                            new PricingAgreementUs50ProcessorCard.Tiered3(
                                new Tiered3
                                {
                                    Fees = new Tiered3Fees
                                    {
                                        MastercardVisaDiscover = new QualRates
                                        {
                                            QualifiedRate = new ProcessorFee(),
                                            MidQualRate = new ProcessorFee(),
                                            NonQualRate = new ProcessorFee(),
                                        },
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
                                UnauthorizedReturn = 500,
                                Statement = 250,
                                MonthlyMinimum = 500,
                                AccountVerification = 10,
                                DiscountRateUnder10000 = 0.5,
                                DiscountRateAbove10000 = 1,
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_4()
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
                  "fees": {
                    "mastercardVisaDiscover": {
                      "qualifiedRate": {},
                      "midQualRate": {},
                      "nonQualRate": {},
                      "premiumRate": {}
                    }
                  },
                  "planType": "tiered4"
                },
                "ach": {
                  "fees": {
                    "transaction": 50,
                    "batch": 5,
                    "returns": 400,
                    "unauthorizedReturn": 500,
                    "statement": 250,
                    "monthlyMinimum": 500,
                    "accountVerification": 10,
                    "discountRateUnder10000": 0.5,
                    "discountRateAbove10000": 1
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
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                            new PricingAgreementUs50ProcessorCard.Tiered4(
                                new Tiered4
                                {
                                    Fees = new Tiered4Fees
                                    {
                                        MastercardVisaDiscover = new QualRatesWithPremium
                                        {
                                            QualifiedRate = new ProcessorFee(),
                                            MidQualRate = new ProcessorFee(),
                                            NonQualRate = new ProcessorFee(),
                                            PremiumRate = new ProcessorFee(),
                                        },
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
                                UnauthorizedReturn = 500,
                                Statement = 250,
                                MonthlyMinimum = 500,
                                AccountVerification = 10,
                                DiscountRateUnder10000 = 0.5,
                                DiscountRateAbove10000 = 1,
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_5()
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
                  "fees": {
                    "mastercardVisaDiscover": {
                      "qualifiedRate": {},
                      "midQualRate": {},
                      "nonQualRate": {},
                      "premiumRate": {},
                      "regulatedCheckCard": {},
                      "unregulatedCheckCard": {}
                    }
                  },
                  "planType": "tiered6"
                },
                "ach": {
                  "fees": {
                    "transaction": 50,
                    "batch": 5,
                    "returns": 400,
                    "unauthorizedReturn": 500,
                    "statement": 250,
                    "monthlyMinimum": 500,
                    "accountVerification": 10,
                    "discountRateUnder10000": 0.5,
                    "discountRateAbove10000": 1
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
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                            new PricingAgreementUs50ProcessorCard.Tiered6(
                                new Tiered6
                                {
                                    Fees = new Tiered6Fees
                                    {
                                        MastercardVisaDiscover =
                                            new QualRatesWithPremiumAndRegulated
                                            {
                                                QualifiedRate = new ProcessorFee(),
                                                MidQualRate = new ProcessorFee(),
                                                NonQualRate = new ProcessorFee(),
                                                PremiumRate = new ProcessorFee(),
                                                RegulatedCheckCard = new ProcessorFee(),
                                                UnregulatedCheckCard = new ProcessorFee(),
                                            },
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
                                UnauthorizedReturn = 500,
                                Statement = 250,
                                MonthlyMinimum = 500,
                                AccountVerification = 10,
                                DiscountRateUnder10000 = 0.5,
                                DiscountRateAbove10000 = 1,
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_6()
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
                "minimum": 0,
                "voiceAuthorization": 95,
                "chargeback": 2500,
                "retrieval": 1500,
                "batch": 0,
                "earlyTermination": 57500
              },
              "processor": {
                "card": {
                  "fees": {
                    "standardCards": {}
                  },
                  "planType": "flatRate"
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                        Minimum = 0,
                        VoiceAuthorization = 95,
                        Chargeback = 2500,
                        Retrieval = 1500,
                        Batch = 0,
                        EarlyTermination = 57500,
                    },
                    Processor = new PricingAgreementUs50Processor
                    {
                        Card = new PricingAgreementUs50ProcessorCard(
                            new PricingAgreementUs50ProcessorCard.FlatRate(
                                new FlatRate
                                {
                                    Fees = new FlatRateFees { StandardCards = new ProcessorFee() },
                                }
                            )
                        ),
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_7()
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
                  "fees": {
                    "monthlySubscription": 1,
                    "volume": 1.25
                  },
                  "planType": "consumerChoice"
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                            new PricingAgreementUs50ProcessorCard.ConsumerChoice(
                                new ConsumerChoice
                                {
                                    Fees = new ConsumerChoiceFees
                                    {
                                        MonthlySubscription = 1,
                                        Volume = 1.25,
                                    },
                                }
                            )
                        ),
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_8()
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
                  "fees": {
                    "monthlySubscription": 1,
                    "debit": {
                      "volume": 1.25,
                      "transaction": 1
                    },
                    "credit": {}
                  },
                  "planType": "rewardPayChoice"
                }
              },
              "services": [
                {
                  "enabled": true,
                  "name": "hardwareAdvantagePlan"
                }
              ],
              "key": "Your-Unique-Identifier",
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

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
                    .WithPath("/pricing-intents")
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

        var response = await Client.Boarding.PricingIntents.CreateAsync(
            new CreatePricingIntentsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PricingIntent50
                {
                    Country = "US",
                    Version = "5.0",
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
                            new PricingAgreementUs50ProcessorCard.RewardPayChoice(
                                new RewardPayChoice
                                {
                                    Fees = new RewardPayChoiceFees
                                    {
                                        MonthlySubscription = 1,
                                        Debit = new RewardPayChoiceFeesDebit
                                        {
                                            Volume = 1.25,
                                            Transaction = 1,
                                        },
                                        Credit = new RewardPayChoiceFeesCredit(),
                                    },
                                }
                            )
                        ),
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
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PricingIntent50>(mockResponse)).UsingDefaults()
        );
    }
}
