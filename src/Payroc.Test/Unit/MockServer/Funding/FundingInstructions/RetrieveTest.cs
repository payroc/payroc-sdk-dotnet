using NUnit.Framework;
using Payroc.Funding.FundingInstructions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingInstructions;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
                        "value": 120000
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

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-instructions/1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingInstructions.RetrieveAsync(
            new RetrieveFundingInstructionsRequest { InstructionId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
