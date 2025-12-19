using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingInstructions;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {}
            """;

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
                        "value": 120000,
                        "currency": "USD"
                      },
                      "status": "accepted",
                      "metadata": {
                        "yourCustomField": "abc123"
                      },
                      "link": {
                        "rel": "fundingAccount",
                        "method": "get",
                        "href": "https://api.payroc.com/v1/funding-accounts/123"
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
                Body = new Instruction(),
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Instruction>(mockResponse)).UsingDefaults()
        );
    }
}
