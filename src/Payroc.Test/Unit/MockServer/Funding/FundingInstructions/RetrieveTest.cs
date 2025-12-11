using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingInstructions;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "instructionId": 64643131,
              "createdDate": "2024-07-02T15:30:00.000Z",
              "lastModifiedDate": "2024-07-02T15:30:00.000Z",
              "status": "accepted",
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
                  ],
                  "link": {
                    "rel": "merchant",
                    "method": "get",
                    "href": "https://api.payroc.com/v1/processing-accounts/4525644354"
                  }
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Instruction>(mockResponse)).UsingDefaults()
        );
    }
}
