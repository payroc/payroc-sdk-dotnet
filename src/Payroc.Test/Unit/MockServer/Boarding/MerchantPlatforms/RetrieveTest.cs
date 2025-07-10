using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.MerchantPlatforms;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "merchantPlatformId": "12345",
              "createdDate": "2024-07-02T12:00:00.000Z",
              "lastModifiedDate": "2024-07-02T12:00:00.000Z",
              "business": {
                "name": "Example Corp",
                "taxId": "xxxxx6789",
                "organizationType": "privateCorporation",
                "countryOfOperation": "US",
                "addresses": [
                  {
                    "address1": "1 Example Ave.",
                    "address2": "Example Address Line 2",
                    "address3": "Example Address Line 3",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056",
                    "type": "legalAddress"
                  }
                ],
                "contactMethods": [
                  {
                    "value": "jane.doe@example.com",
                    "type": "email"
                  }
                ]
              },
              "processingAccounts": [
                {
                  "processingAccountId": "38765",
                  "doingBusinessAs": "Pizza Doe",
                  "status": "approved",
                  "link": {
                    "rel": "processingAccount",
                    "href": "https://api.payroc.com/v1/processing-accounts/38765",
                    "method": "get"
                  },
                  "signature": {
                    "link": {
                      "rel": "agreement",
                      "method": "get",
                      "href": "https://us.agreementexpress.net/mv2/viewer2.jsp?docId=00000000-0000-0000-0000-000000000000"
                    },
                    "type": "requestedViaDirectLink"
                  }
                }
              ],
              "metadata": {
                "customerId": "2345"
              },
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/merchant-platforms/12345")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.MerchantPlatforms.RetrieveAsync(
            new RetrieveMerchantPlatformsRequest { MerchantPlatformId = "12345" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<MerchantPlatform>(mockResponse)).UsingDefaults()
        );
    }
}
