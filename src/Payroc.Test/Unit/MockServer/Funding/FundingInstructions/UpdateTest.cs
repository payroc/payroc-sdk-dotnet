using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingInstructions;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "merchants": [
                {
                  "merchantId": "9876543219",
                  "recipients": [
                    {
                      "fundingAccountId": 124,
                      "paymentMethod": "ACH",
                      "amount": {
                        "value": 69950,
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
                    .WithPath("/funding-instructions/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Funding.FundingInstructions.UpdateAsync(
                new UpdateFundingInstructionsRequest
                {
                    InstructionId = 1,
                    Body = new Instruction
                    {
                        Merchants = new List<InstructionMerchantsItem>()
                        {
                            new InstructionMerchantsItem
                            {
                                MerchantId = "9876543219",
                                Recipients = new List<InstructionMerchantsItemRecipientsItem>()
                                {
                                    new InstructionMerchantsItemRecipientsItem
                                    {
                                        FundingAccountId = 124,
                                        PaymentMethod =
                                            InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                                        Amount = new InstructionMerchantsItemRecipientsItemAmount
                                        {
                                            Value = 69950,
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
            )
        );
    }
}
