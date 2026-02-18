using NUnit.Framework;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class ListAccountsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            [
              {
                "type": "checking",
                "use": "credit",
                "nameOnAccount": "Jane Doe",
                "paymentMethods": [
                  {
                    "type": "ach",
                    "value": {
                      "routingNumber": "123456789",
                      "accountNumber": "1234567890"
                    }
                  }
                ],
                "metadata": {
                  "yourCustomField": "abc123"
                }
              },
              {
                "type": "checking",
                "use": "debit",
                "nameOnAccount": "Jane Doe",
                "paymentMethods": [
                  {
                    "type": "ach",
                    "value": {
                      "routingNumber": "123456789",
                      "accountNumber": "1234567890"
                    }
                  }
                ],
                "metadata": {
                  "yourCustomField": "abc123"
                }
              }
            ]
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1/funding-accounts")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingRecipients.ListAccountsAsync(
            new ListFundingRecipientFundingAccountsRequest { RecipientId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
