using NUnit.Framework;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.MerchantPlatforms;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
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
                    "type": "email",
                    "value": "jane.doe@example.com"
                  }
                ]
              },
              "metadata": {
                "customerId": "2345"
              }
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
        JsonAssert.AreEqual(response, mockResponse);
    }
}
