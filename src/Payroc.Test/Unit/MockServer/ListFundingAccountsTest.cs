using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class ListFundingAccountsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            [
              {
                "fundingAccountId": 123,
                "createdDate": "2024-01-15T09:30:00.000Z",
                "lastModifiedDate": "2024-07-02T12:00:00.000Z",
                "status": "approved",
                "type": "checking",
                "use": "creditAndDebit",
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
                    "href": "https://api.payroc.com/v1/processing-accounts/38765"
                  }
                ]
              },
              {
                "fundingAccountId": 124,
                "createdDate": "2024-07-02T12:00:00.000Z",
                "lastModifiedDate": "2024-07-02T12:00:00.000Z",
                "status": "pending",
                "type": "checking",
                "use": "creditAndDebit",
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
                    "href": "https://api.payroc.com/v1/processing-accounts/38765"
                  }
                ]
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

        var response = await Client.Boarding.ProcessingAccounts.ListFundingAccountsAsync(
            new ListProcessingAccountFundingAccountsRequest { ProcessingAccountId = "38765" },
            RequestOptions
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IEnumerable<FundingAccount>>(mockResponse))
                .UsingPropertiesComparer()
        );
    }
}
