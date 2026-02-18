using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingInstructions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingInstructions;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "yourCustomField": "abc123"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        const string mockResponse = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "yourCustomField": "abc123"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "instructionRef": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-instructions")
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

        var response = await Client.Funding.FundingInstructions.CreateAsync(
            new CreateFundingInstructionsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new Instruction
                {
                    Merchants = new List<InstructionMerchantsItem>()
                    {
                        new InstructionMerchantsItem
                        {
                            MerchantId = "4525644354",
                            Recipients = new List<InstructionMerchantsItemRecipientsItem>()
                            {
                                new InstructionMerchantsItemRecipientsItem
                                {
                                    FundingAccountId = 123,
                                    PaymentMethod =
                                        InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                                    Amount = new InstructionMerchantsItemRecipientsItemAmount
                                    {
                                        Value = 120000,
                                        Currency =
                                            InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
                                    },
                                    Metadata = new Dictionary<string, string>()
                                    {
                                        { "yourCustomField", "abc123" },
                                    },
                                },
                            },
                        },
                    },
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "supplier": "IT Support Services"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "instructionCreatedBy": "Jane Doe"
              }
            }
            """;

        const string mockResponse = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "yourCustomField": "abc123"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "instructionRef": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-instructions")
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

        var response = await Client.Funding.FundingInstructions.CreateAsync(
            new CreateFundingInstructionsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new Instruction
                {
                    Merchants = new List<InstructionMerchantsItem>()
                    {
                        new InstructionMerchantsItem
                        {
                            MerchantId = "4525644354",
                            Recipients = new List<InstructionMerchantsItemRecipientsItem>()
                            {
                                new InstructionMerchantsItemRecipientsItem
                                {
                                    FundingAccountId = 123,
                                    PaymentMethod =
                                        InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                                    Amount = new InstructionMerchantsItemRecipientsItemAmount
                                    {
                                        Value = 120000,
                                        Currency =
                                            InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
                                    },
                                    Metadata = new Dictionary<string, string>()
                                    {
                                        { "supplier", "IT Support Services" },
                                    },
                                },
                            },
                        },
                    },
                    Metadata = new Dictionary<string, string>()
                    {
                        { "instructionCreatedBy", "Jane Doe" },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_3()
    {
        const string requestJson = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "yourCustomField": "abc123"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        const string mockResponse = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "yourCustomField": "abc123"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "instructionRef": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-instructions")
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

        var response = await Client.Funding.FundingInstructions.CreateAsync(
            new CreateFundingInstructionsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new Instruction
                {
                    Merchants = new List<InstructionMerchantsItem>()
                    {
                        new InstructionMerchantsItem
                        {
                            MerchantId = "4525644354",
                            Recipients = new List<InstructionMerchantsItemRecipientsItem>()
                            {
                                new InstructionMerchantsItemRecipientsItem
                                {
                                    FundingAccountId = 123,
                                    PaymentMethod =
                                        InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                                    Amount = new InstructionMerchantsItemRecipientsItemAmount
                                    {
                                        Value = 120000,
                                        Currency =
                                            InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
                                    },
                                    Metadata = new Dictionary<string, string>()
                                    {
                                        { "yourCustomField", "abc123" },
                                    },
                                },
                            },
                        },
                    },
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_4()
    {
        const string requestJson = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "yourCustomField": "abc123"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        const string mockResponse = """
            {
              "merchants": [
                {
                  "merchantId": "4525644354",
                  "recipients": [
                    {
                      "fundingAccountId": 123,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 120000,
                        "currency": "USD"
                      },
                      "metadata": {
                        "supplier": "IT Support Services"
                      }
                    }
                  ]
                }
              ],
              "metadata": {
                "instructionCreatedBy": "Jane Doe"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-instructions")
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

        var response = await Client.Funding.FundingInstructions.CreateAsync(
            new CreateFundingInstructionsRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new Instruction
                {
                    Merchants = new List<InstructionMerchantsItem>()
                    {
                        new InstructionMerchantsItem
                        {
                            MerchantId = "4525644354",
                            Recipients = new List<InstructionMerchantsItemRecipientsItem>()
                            {
                                new InstructionMerchantsItemRecipientsItem
                                {
                                    FundingAccountId = 123,
                                    PaymentMethod =
                                        InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                                    Amount = new InstructionMerchantsItemRecipientsItemAmount
                                    {
                                        Value = 120000,
                                        Currency =
                                            InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
                                    },
                                    Metadata = new Dictionary<string, string>()
                                    {
                                        { "yourCustomField", "abc123" },
                                    },
                                },
                            },
                        },
                    },
                    Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
