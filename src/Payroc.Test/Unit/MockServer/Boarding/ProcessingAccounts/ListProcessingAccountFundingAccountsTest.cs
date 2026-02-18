using NUnit.Framework;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class ListProcessingAccountFundingAccountsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            [
              {
                "type": "checking",
                "use": "creditAndDebit",
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
                "use": "creditAndDebit",
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
                    .WithPath("/processing-accounts/38765/funding-accounts")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response =
            await Client.Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsAsync(
                new ListProcessingAccountFundingAccountsRequest { ProcessingAccountId = "38765" }
            );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
