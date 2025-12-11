using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "recipientId": 234,
              "status": "approved",
              "createdDate": "2024-07-02T15:30:00.000Z",
              "lastModifiedDate": "2024-07-02T15:30:00.000Z",
              "recipientType": "privateCorporation",
              "taxId": "123456789",
              "charityId": "charityId",
              "doingBusinessAs": "Pizza Doe",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "value": "2025550164",
                  "type": "phone"
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              },
              "owners": [
                {
                  "ownerId": 4564,
                  "link": {
                    "rel": "owner",
                    "href": "https://api.payroc.com/v1/owners/4564",
                    "method": "get"
                  }
                }
              ],
              "fundingAccounts": [
                {
                  "fundingAccountId": 123,
                  "status": "approved",
                  "link": {
                    "rel": "fundingAccount",
                    "href": "https://api.payroc.com/v1/funding-accounts/123",
                    "method": "get"
                  }
                },
                {
                  "fundingAccountId": 124,
                  "status": "hold",
                  "link": {
                    "rel": "fundingAccount",
                    "href": "https://api.payroc.com/v1/funding-accounts/124",
                    "method": "get"
                  }
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingRecipients.RetrieveAsync(
            new RetrieveFundingRecipientsRequest { RecipientId = 1 }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<FundingRecipient>(mockResponse)).UsingDefaults()
        );
    }
}
