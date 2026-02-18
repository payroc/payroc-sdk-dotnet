using NUnit.Framework;
using Payroc.Funding.FundingAccounts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingAccounts;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
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
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/funding-accounts/1").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingAccounts.RetrieveAsync(
            new RetrieveFundingAccountsRequest { FundingAccountId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
