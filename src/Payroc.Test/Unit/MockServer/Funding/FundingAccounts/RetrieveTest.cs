using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingAccounts;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingAccounts;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "fundingAccountId": 123,
              "createdDate": "2024-07-02T15:30:00.000Z",
              "lastModifiedDate": "2024-07-02T15:30:00.000Z",
              "status": "pending",
              "type": "checking",
              "use": "credit",
              "nameOnAccount": "Jane Doe",
              "paymentMethods": [
                {
                  "value": {
                    "routingNumber": "123456789",
                    "accountNumber": "1234567890"
                  },
                  "type": "ach"
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              },
              "links": [
                {
                  "rel": "parent",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/merchants/4525644354"
                }
              ]
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<FundingAccount>(mockResponse)).UsingDefaults()
        );
    }
}
